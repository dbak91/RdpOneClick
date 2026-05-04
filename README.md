# **RDP Auto Click Tool**

## **Overview**

Automates clicking through the new RDP security prompt.   

## **Dependencies**

- V4+ Requires .NET 4.8 framework which is usually present on most Windows 10 and 11 systems

## Security notice
- Officially you should be using signed rdp files
- Microsoft support a cleaner admin only registry fix editing HKEY LOCAL MACHINE but have said it may be patched out
- Since this is a security pop up it is automating it may get blocked one day. The intention to increase security a little bit is to ensure the machine name the user expects is still in the rdp file by adding a new "connectTo" parameter and verifying the rdp matches. This atleast means the user has passed and checked the machine hasnt changed. (tbd)


## In action

Youtube    watch?v=VETpTN30J1Y

## C# Source Notes
95% Visual Basic Human-developed, (assistance in stopping cmd pop ups) <br>
100% C# LLM-Claude-converted. 
- First developed and ironed out behaviour in Visual Basic, then after realising dependencies and file size, the source code was forwarded to Claude LLM asking for conversion to C#. 

## Version differences
#### V4.0
The first NET 4.8 dependant version and the first version to automate the clicking by button name. 
Flaws:
NET 4.8 must be installaled. (Already present on most moderrn systems)

#### V3.0 / V3.2
This is the first c# versions and the last 2 versions based on clicking at x and y coordinates as oppose to button name 
Note: 
- These have not been tested thoroughly for dependencies. The target was NET 8.0 framework so it is possible NET 8 may be required.
Flaws:  
- Click locations require fixed resolution so any resolution change (such as going from wfh to office) will need their own shortcuts.

#### V2.0
This is the last self contained visual basic version.
Flaws:  
- Large file size and slow to start until cached.
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





## **Tip**:

Optionally change the icon of the shortcut to be RDP icon. 'Richt Click - Properties - Change Icon - Browse - C:\\Windows\\System32\\mstsc.exe  


## **Instructions**

#### Path mode
---

**Names of checkboxes**: 
- Open the rdp file to force the prompt and inspect and note down the checkbox names you want to pre click. (Case sensitive)
- If names are non-simple (e.g. more than one word or with qualifiers) run GetNamesOfButtons.exe to examine the  exact name of the checkboxes you want. 
- Close rdp (and any getnames function) after noting down names

**Rdp path**  
- Right click rdp file and choose 'Copy as path'

**Finally add parameters**  
-  Download and unzip RdpAutoClick.exe to a suitable location
-  If happy with the .exe's location, double-click or open the exe to set any warning triggers.
  E.g) If Windows smartscreen 'protected' your pc and stopped the exe running, click 'More info' then 'Run anyway' to prevent this dialogue in the future.  
-  Right click .exe and choose 'Send To...Desktop (create shortcut)' and name the shortcut after your rdp vm name
-  Right click the shortcut on Desktop and click 'Properties'
-  In the Traget field, add Rdp Path as the first parameter in quotes, and any desired checkbox names, without quotes if single word or with quotes if contains spaces.  
e.g.) Target:# RdpAutoClick.exe "C:\Users\Dave\Desktop\WIN10.rdp" Clipboard Drives (This will pre click Clipboard and Drives)
- Save and test. 
  
    
	
#### Click mode
---
  	
**Get X and Y coordinates**
- Copy contents of 'powershell\_command.find\_pos.txt' into Powershell and press enter confirming any 'multi-line' warnings
-  Keeping powershell visible, hover over the rdp file and note down the x and y values then open to force the prompt, similarly for any wanted pre-clicks such as clipboard as well as the final connect button position note down the x and y values.

**Add parameters**
- Extract RdpAutoClick.exe to a suitable loacation
-  Create desktop shortcut of .exe and name it after your chosen rdp file/vm name
- Edit desktop shortcut properties Target field to include parameters: See Usage,
E.g.) Target:# RdpAutoClick.exe "C:\\USERS\\BAKERDA\\Desktop\\WIN10.rdp"  1104 659 768 561 (This expects the rdp at 1104 659 and the connect button at 768 561 with no pre clicks defined



## Tips
#### Getting Coordinates
Use a pen or phone to note down the coordinates while hovering windows cursor over the buttons. If prefer windows a tip is to have it so a .txt pad, the security pop up and the LHS of Powershell are all cleanly visible. Click txt pad first to type, hover over button, examine Powershell without clicking anywhere and type.
#### Too many desktop icons
Move original rdp files to a folder and use the tools Path Mode.
#### Repetitive setup
- When editing target field use Ctrl A and copy off the small form field into a text editor and Ctrl A Del. Ctrl V to replace when finished adding coordinates 
- Use path mode
- Have all rdps in the same directory
