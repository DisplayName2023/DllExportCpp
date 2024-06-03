from os.path import join
from conan import ConanFile, tools
from conan.tools.files import copy
import os

class ProjectCppCliConan(ConanFile):
    name = "evaluatorcppcli"
    version = "0.0.1-1"
    # settings = "os", "compiler", "build_type", "arch"
    description = "EvaluatorCppCli.dll for Windows"
    url = "None"
    license = "MIT"
    author = "None"
    topics = None
    short_paths = True

    def package(self):
        if os.path.exists(join(self.source_folder, '../EvaluatorCppCli')):
            copy(self, "*.h",  join(self.source_folder, '.'), join(self.package_folder, "include"), keep_path=False, excludes=("obj*", "x64*"))
            copy(self, "*.lib",  join(self.source_folder, '../x64/Release'), join(self.package_folder, "lib"), keep_path=False)
            copy(self, "*.dll",  join(self.source_folder, '../x64/Release'), join(self.package_folder, "bin"), keep_path=False)
            copy(self, "*.runtimeconfig.json", join(self.source_folder, '../x64/Release'), join(self.package_folder, "bin"), keep_path=False)
        else:
            copy(self, "*.h",  self.source_folder, join(self.package_folder, "include"), keep_path=False)
            copy(self, "*.lib",  self.source_folder, join(self.package_folder, "lib"), keep_path=False)
            copy(self, "*.dll",  self.source_folder, join(self.package_folder, "bin"), keep_path=False)
            copy(self, "*.runtimeconfig.json",  self.source_folder, join(self.package_folder, "bin"), keep_path=False)


    def package_info(self):
        self.cpp_info.libs = tools.files.collect_libs(self)

    def package_id(self):
        pass