# **RDP Auto Click Tool**

## **Overview**

Automates clicking through the new RDP security prompt.   

## In action

Youtube    watch?v=VETpTN30J1Y

## C# Source Notes
95% Visual Basic Human-developed, (assistance in stopping cmd pop ups) <br>
100% C# LLM-Claude-converted. 
- First developed and ironed out behaviour in Visual Basic, then after realising dependencies and file size, the source code was forwarded to Claude LLM asking for conversion to C#. 


## **Modes**

#### Path mode (most reliable)  


RdpAutoClick.exe "C:\\file.rdp" \[optional checkbox option names]


#### Click mode  


RdpAutoClick.exe click \[rdpX] \[rdpY] \[connectX] \[connectY] \[optional pre click x and ys]



## **Example**


#### Path mode 

**RdpAuoClick.exe "C:USERS\\BAKERDA\\Desktop\\Win10.rdp" Clipboard Drives**
  
  
This will pre-click checkboxes named 'Clipboard' and 'Drives' then connect


#### Click Mode:

**RdpAutoClick.exe click 800 500 1100 650 900 520**
  
  
This will double click at '800 500' to open the rdp, then click at '900 520' preclick then finally the connect button at '1100 650'


## **Dependencies**

- Requires .NET 4.8 framework which is usually present on most Windows 10 and 11 systems  


## **Tip**:

Optionally change the icon of the shortcut to be RDP icon. 'Richt Click - Properties - Change Icon - Browse - C:\\Windows\\System32\\mstsc.exe  


## **Instructions**

#### Path mode
---

**Names of checkboxes**: 
1. Open the rdp file to force the prompt and inspect and note down the checkbox names you want to pre click. (Case sensitive)
2. If names are non-simple (e.g. more than one word or with qualifiers) run GetNamesOfButtons.exe to examine the  exact name of the checkboxes you want. 
3. Close rdp (and any getnames function) after noting down names

**Rdp path**
4. Right click rdp file and choose 'Copy as path'

**Finally add parameters**
5. Download and unzip RdpAutoClick.exe to a suitable location
6. Right click .exe and choose 'Send To...Desktop (create shortcut)'
7  Right click the shortcut on Desktop and click 'Properties'
8. Add Rdp Path as the first parameter in quotes, and any desired checkbox names, without quotes if single word or with quotes if contains spaces, to the properties 'Target' field.  
e.g.) Target:# RdpAutoClick.exe "C:\Users\Dave\Desktop\WIN10.rdp" Clipboard Drives (This will pre click Clipboard and Drives)
9. Save and test. 
  
    
	
#### Click mode
---
  	
**Get X and Y coordinates**
1. Copy contents of 'powershell\_command.find\_pos.txt' into Powershell and press enter confirming any 'multi-line' warnings
2. Keeping powershell visible, hover over the rdp file and note down the x and y values then open to force the prompt, similarly for any wanted pre-clicks such as clipboard as well as the final connect button position note down the x and y values.

**Add parameters**
3. Extract RdpAutoClick.exe to a suitable loacation
4. Create desktop shortcut of .exe and name it after your chosen rdp file/vm name
5. Edit desktop shortcut properties Target field to include parameters: See Usage,
E.g.) Target:# RdpAutoClick.exe "C:\\USERS\\BAKERDA\\Desktop\\WIN10.rdp"  1104 659 768 561 (This expects the rdp at 1104 659 and the connect button at 768 561 with no pre clicks defined


## Security notice
- Officially you should be using signed rdp files
- Microsoft support a cleaner admin only registry fix editing HKEY LOCAL MACHINE but have said it may be patched out
- Since this is a security pop up it is clicking it may get blocked one day. The intention to increase security a little bit is to ensure the machine name the user expects is still in the rdp file by adding a new "connectTo" parameter and verifying the rdp matches. This atleast means the user has passed and checked the machine hasnt changed.

## Tips
# Getting Coordinates
Use a pen or phone to note down the coordinates while hovering windows cursor over the buttons. If prefer windows a tip is to have it so a .txt pad, the security pop up and the LHS of Powershell are all cleanly visible. Click txt pad first to type, hover over button, examine Powershell without clicking anywhere and type.
## Too many desktop icons
Move original rdp files to a folder and use the tools Path Mode.
## Repetitive setup
- When editing target field use Ctrl A and copy off the small form field into a text editor and Ctrl A Del. Ctrl V to replace when finished adding coordinates 
- Use path mode
- Have all rdps in the same directory

  
# RDP Auto Click Tool

## Overview

Automates clicking through the new RDP security prompt.

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
2. Keeping powershell visible, hover over the rdp file and note down the x and y values then open, similarly for any wanted pre-clicks such as clipboard as well as the final connect button position.<br>
2.b If Using path mode, right click rdp file and choose "copy as path"
4. Extract RdpAutoClickExe.7z to a suitable location.
5. Create desktop shortcut of .exe and name it after your chosen rdp file/vm name
6. Edit desktop shortcut properties target field to include parameters: See Usage, <br>
E.g.) Target: # C:\\Users\\BAKERDA\\RdpAutoClick.exe "C:\\USERS\\BAKERDA\\Desktop\\WIN10.rdp"  1104 659 768 561
7. Copy the shortcut for as many rdp files as you have, editing the coordinate parameters in each.
8. Optionally change the shortcut icon of the shortcut to be rdp by using mstsc.exe <br>'Right click - Properties - Icon - Browse - C:\\Windows\\System32\\mstsc.exe'


- Fully setup one shortcut first and copy it multiple times
- Edit only the rdp filename and if appropriate any different clicks (some will likely remain the same).
