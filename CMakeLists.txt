cmake_minimum_required(VERSION 3.17)
project(Fuzzy_Sorting)

set(CMAKE_CXX_STANDARD 17)

add_executable(Fuzzy_Sorting main.cpp public/Logger.h public/FuzzySorter.h public/QuickSorter.h public/BubbleSorter.h)