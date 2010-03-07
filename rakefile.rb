COMPILE_TARGET = "debug"
require "build_support/BuildUtils.rb"

include FileTest
require 'albacore'

RESULTS_DIR = "results"
BUILD_NUMBER = "0.1.0."  + (ENV["BUILD_NUMBER"].nil? ? '0' : ENV["BUILD_NUMBER"].to_s)
PRODUCT = "NSBManager"
COPYRIGHT = 'Copyright 2010 Andreas Ohlund, Jens Pettersson, Daniel Kjellqvist, et al. All rights reserved.';
COMMON_ASSEMBLY_INFO = 'src/CommonAssemblyInfo.cs';
CLR_VERSION = "v3.5"

versionNumber = ENV["BUILD_NUMBER"].nil? ? 0 : ENV["BUILD_NUMBER"]

props = { :archive => "build" }


desc "Compiles, unit tests"
task :all => [:default]

desc "**Default**, compiles and runs tests"
task :default => [:compile, :unit_test]

desc "Update the version information for the build"
assemblyinfo :version do |asm|
  asm.version = BUILD_NUMBER
  asm.custom_attributes :AssemblyInformationalVersion => BUILD_NUMBER
  asm.product_name = PRODUCT
  asm.copyright = COPYRIGHT
  asm.output_file = COMMON_ASSEMBLY_INFO

  begin
    commit = (ENV["BUILD_VCS_NUMBER"].nil? ? `git log -1 --pretty=format:%H` : ENV["BUILD_VCS_NUMBER"])
  rescue
    commit = "git unavailable"
  end
  asm.trademark = commit
end

desc "Prepares the working directory for a new build"
task :clean do
	#TODO: do any other tasks required to clean/prepare the working directory
	FileUtils.rm_r props[:archive], :force=>true
        Dir.mkdir props[:archive]
	Dir.mkdir props[:archive]+'/UserInterface'
	Dir.mkdir props[:archive]+'/Service'
	Dir.mkdir props[:archive]+'/Instrumentation'
end

desc "Compiles the app"
task :compile => [:clean, :version] do
  MSBuildRunner.compile :compilemode => COMPILE_TARGET, :solutionfile => 'src/NSBManager.sln', :clrversion => CLR_VERSION
  
  copyOutputFiles "src/NSBManager.UserInterface/bin/#{COMPILE_TARGET}", "*.{dll,pdb,config}", props[:archive] + '/UserInterface/'
  copyOutputFiles "src/NSBManager.ManagementService/bin/#{COMPILE_TARGET}", "*.{dll,pdb,config}", props[:archive] + '/Service/'
  copyOutputFiles "src/NSBManager.Instrumentation/bin/#{COMPILE_TARGET}", "*.{dll,pdb}", props[:archive] + '/Instrumentation/'
end

def copyOutputFiles(fromDir, filePattern, outDir)
  Dir.glob(File.join(fromDir, filePattern)){|file| 		
	copy(file, outDir) if File.file?(file)
  } 
end

desc "Runs unit tests"
task :test => [:unit_test]

desc "Runs unit tests"
task :unit_test => :compile do
  runner = NUnitRunner.new :compilemode => COMPILE_TARGET, :source => 'src', :platform => 'x86'
  runner.executeTests ['NSBManager.ManagementService.UnitTests', 'NSBManager.ManagementService.UnitTests', 'NSBManager.Instrumentation.UnitTests']  

  mspecrunner = MSpecRunner.new :compilemode => COMPILE_TARGET, :source => 'src'
  mspecrunner.executeTests ['NSBManager.ManagementService.UnitTests']  

end

desc "Target used for the CI server"
task :ci => [:unit_test,:zip]

desc "ZIPs up the build results"
zip do |zip|
	zip.directories_to_zip = [props[:archive]]
	zip.output_file = 'fubumvc.zip'
	zip.output_path = 'build'
end