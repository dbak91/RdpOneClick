# RDP Auto Click Tool

## Overview

Automates clicking through the new RDP security prompt.


## C# Source Notes
95% Visual Basic Human-developed, 100% C# LLM-(claude)-converted. 
First developed and ironed out behaviour in Visual Basic, then after realising dependencies and file size the source code was forwarded to Claude LLM asking for conversion to C#. 

## Modes

### Path mode (most reliable)

RdpAutoClick.exe "C:\\file.rdp" \[connectX] \[connectY] \[optional pre click x and ys]



### Click mode

RdpAutoClick.exe **click** \[rdpX] \[rdpY] \[connectX] \[connectY] \[optional pre click x and ys]



## Example



Path mode



RdpAuoClick.exe "C:USERS\\BAKERDA\\Desktop\\Win10.rdp" 100 650 900 520



Click Mode:



RdpAutoClick.exe click 800 500 1100 650 900 520



## Notes

* Requires fixed screen resolution
* Coordinates must be calibrated
* Desktop must be cleanly visisble

## Instructions

1. Copy contents of 'powershell\_command.find\_pos.txt' into Powershell and press enter confirming any 'multi-line' warnings
2. Keeping powershell visible, hover over the rdp file and note down the x and y values then open, similarly for any wanted pre-clicks such as clipboard as well as the final connect button position.
2.b If Using path mode, right click rdp file and choose "copy as path"
3. Extract RdpAuto2Exe.zip to a suitable location.
4. Create desktop shortcut of .exe and name it after your chosen rdp file/vm name
5. Edit desktop shortcut properties target to include parameters: See Usage,
E.g.) Target: # C:\\Users\\BAKERDA\\RdpAuto2.exe "C:\\USERS\\BAKERDA\\Desktop\\WIN10.rdp"  1104 659 768 561
6. Copy the shortcut for as many rdp files as you have, editing the coordinate parameters in each.
7. Optionally change the shortcut icon of the shortcut to be rdp by using mstc.exe Right click - Properties - Icon - Browse - C:\\Windows\\System32\\mstc.exe
