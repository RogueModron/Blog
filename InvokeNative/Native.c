#include "Native.h"

void DoWithNoParameters() {
    return;
}

int DoWithInt(int a, int b) {
    return a + b;
}

double DoWithDouble(double a, double b) {
    return a + b;
}

void DoWithIntPointer(int a, int b, int* r) {
    *r = a + b;
}

void DoWithDoublePointer(double a, double b, double* r) {
    *r = a + b;
}
