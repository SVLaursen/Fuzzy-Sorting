//
// Description
//

#ifndef FUZZY_SORTING_LOGGER_H
#define FUZZY_SORTING_LOGGER_H

class Logger{
public:
    static long GetMemoryUsage(std::array &array){
        return sizeof(array);
    }

    static long GetMemoryUsageInMegabytes(std::array &array){
        return sizeof(array) / 1048576;
    }
};

#endif //FUZZY_SORTING_LOGGER_H
