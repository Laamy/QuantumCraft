#pragma region Includes

#include <windows.h>
#include <vector>
#include <string>
#include <Psapi.h>
#include <sstream>
#include <thread>
#include <queue>
#include <map>
#include <iostream>
#include <mutex>

#include "Libs/minhook/minhook.h"

#include "Utils/MemoryUtils.h"

#include "FileIO.h"

#include "Game.h"

#include "Hook/FuncHook.h"

#include "Console.h"

#pragma endregion

void Init(HANDLE module) {
    Console::CreateConsole("QuantumCraft");
    //InitHooks();
}

BOOL APIENTRY DllMain(HMODULE module, DWORD reason, LPVOID reserved)
{
    if (reason == DLL_PROCESS_ATTACH) {
        CreateThread(0, 0, (LPTHREAD_START_ROUTINE)Init, module, 0, 0);
    }
    return TRUE;
}