# RDP Auto Click Tool

## Overview
Automates clicking through the new RDP security prompt.

## Modes

### Path mode (most reliable)

RdpAutoClick.exe "C:\file.rdp" <connectX> <connectY> <optional pre click x and ys>


### Click mode

RdpAutoClick.exe click <rdpX> <rdpY> <connectX> <connectY> <optional pre click x and ys>



## Example


RdpAutoClick.exe click 800 500 1100 650 900 520


## Notes
- Requires fixed screen resolution
- Coordinates must be calibrated
- Desktop must be cleanly visisble

## Instructions
1. Copy contents of 'powershell_command.find_pos.txt' into Powershell and press enter confirming any 'multi-line' warnings 
2. Keeping powershell visible, hover over the rdp file and note down the x and y values then open, similarly for any wanted pre-clicks such as clipboard as well as the final connect button position.
2.b If Using path mode, right click rdp file and choose "copy as path"
3. Extract ...exe.zip to a suitable location. 
4. Create desktop shortcut of .exe and name it after your chosen rdp file/vm name
3. Edit desktop shortcut properties target to include paramters: See Usage,
E.g.) Target: # C:\Users\BAKERDA\RdpAuto2.exe click "C;\USERS\BAKERDA\Desktop\WIN10.rdp"  1104 659 768 561
4. Copy the shortcut for as many rdp files as you have, editing the coordinate parameters in each. 
5. Optionally change the icon of the shortcut to not be the bland exe thumbnail. If you can find the rdp icon use that!