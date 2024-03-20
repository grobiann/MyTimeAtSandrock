@echo off

XCOPY /y "../../../MyTimeAtSandrock/PacketGenerator/proto" %cd%
START protoc.exe -I=./ --csharp_out=./ ./Protocol.proto
START ../../../MyTimeAtSandrock/PacketGenerator/bin/Debug/net8.0/PacketGenerator.exe "Protocol.proto"

XCOPY /Y Protocol.cs "../../../MyTimeAtSandrock/Server/Packet"
XCOPY /Y Protocol.cs "../../../MyTimeAtSandrock/Dummyclient/Packet"
XCOPY /Y Protocol.cs "../../../MyTimeAtSandrock_UnityClient/Assets/04_Scripts/Packet"
XCOPY /Y ServerPacketManager.cs "../../../MyTimeAtSandrock/Server/Packet"
XCOPY /Y ClientPacketManager.cs "../../../MyTimeAtSandrock/Dummyclient/Packet"
XCOPY /Y ClientPacketManager.cs "../../../MyTimeAtSandrock_UnityClient/Assets/04_Scripts/Packet"
