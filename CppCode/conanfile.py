from os.path import join
from conan import ConanFile
from conan.tools.cmake import CMake, CMakeDeps, CMakeToolchain, cmake_layout
from conan.tools.files import copy, collect_libs
from pathlib import Path



class ProjectConan(ConanFile):
    url = 'https://github.com/DisplayName2023/EvaluatorCpp'
    license = 'MIT'
    description = 'Evaluator C++/CLI Wrapper'
    name = "EvaluatorCpp"
    version = "0.0.1-1"
    settings = "os", "arch", "build_type", "compiler"
    generators = "CMakeDeps", "CMakeToolchain"
    short_paths = True
    build_policy = "missing"

    options = { "shared": [True, False], 
                "fPIC": [True, False],
                "EHa": [False, True]
               }
    default_options = {"shared": True, 
                       "fPIC": True,
                       "EHa": False 
                       }
    
    exports_sources = "CMakeLists.txt", "conanfile.py", "src*", "cmake*", "!build"
    requires = "ms-gsl/4.0.0", "evaluatorcppcli/0.0.1-1"



        
    def requirements(self):    
        
        if not self.in_local_cache:
            self.requires('gtest/1.14.0')


    
    def config_options(self):
        if self.settings.os == "Windows":
            del self.options.fPIC
        




    def layout(self):
        cmake_layout(self, src_folder=".")
        self.folders.generators = "build"
        self.cpp.build.libdirs = "lib" # write the .libs to the library folder under build
        self.cpp.build.bindirs = "bin" # write the .dll to the bin folder under build

    def generate(self):
         
        for dep in self.dependencies.values():
            print(dep.cpp_info.libdirs[0])

            copy(self, "Evaluator*/*", src=dep.cpp_info.includedirs[0], dst="include", keep_path=True)  # for C++/CLI project
            copy(self, "*.lib", src=dep.cpp_info.libdirs[0], dst=join(self.cpp.build.libdirs, ""), keep_path=False)  
            copy(self, "*.dll", src=dep.cpp_info.bindirs[0], dst=join(self.cpp.build.bindirs, ""), keep_path=False)
            copy(self, "*.bin", src=dep.cpp_info.bindirs[0], dst=join(self.cpp.build.bindirs, ""), keep_path=False)

        tc = CMakeToolchain(self)


        if self.options.shared == False:
            tc.preprocessor_definitions["EvaluatorCppCli_STATIC"] = 1



        file_version = self.version.replace("-", ".").split(".")
        while len(file_version) < 4:
            file_version.append("0")
        tc.cache_variables["VER_MAJOR"] = file_version[0]
        tc.cache_variables["VER_MINOR"] = file_version[1]
        tc.cache_variables["VER_PATCH"] = file_version[2]
        tc.cache_variables["VER_TWEAK"] = file_version[3]

        tc.cache_variables['FILE_VERSION'] = '.'.join(file_version)
        tc.cache_variables['COMPANY_NAME'] = 'Windows'


        tc.generate()


        deps = CMakeDeps(self)
        deps.generate()

    def build(self):
        cmake = CMake(self)



        cmake.configure()
        cmake.build()

    def package_id(self):
        if self.info.options.shared:
            del self.info.settings.compiler
            del self.info.settings.build_type
            self.info.requires.clear()


    def package(self):
        cmake = CMake(self)
        cmake.install()

        copy(self, "*.h", join(self.source_folder, 'src'), join(self.package_folder, "include/EvaluatorCpp"), keep_path=True)
        copy(self, "*Cpp.lib", self.build_folder, join(self.package_folder, "lib"), keep_path=False)
        copy(self, "*Cpp.dll", self.build_folder, join(self.package_folder, "bin"), keep_path=False)


    def package_info(self):
        self.cpp_info.libs = collect_libs(self)
