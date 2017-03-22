LOCAL_PATH := $(call my-dir)

include $(CLEAR_VARS)

LOCAL_MODULE := Native
LOCAL_SRC_FILES := ..\..\Native\Wrapper.cpp

LOCAL_CPPFLAGS += -fexceptions
LOCAL_C_INCLUDES := C:\boost_1_49_0

include $(BUILD_SHARED_LIBRARY)
