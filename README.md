
# UE4 Tools
UE4 tools is a set of tools created to perform several tedious tasks related to UE4 development.


## Features
- Fully Rename a C++ Project
- Rename a C++ class and all references
- Generate new C++ module
- Generate custom Assets with factory classes
- Backup project files from inside the application
- Generate editor code for custom assets e.g. categories, tooltips, custom context actions(right-click actions) and colours.

## Download Versions
Built Program Download: https://samcarey.itch.io/ue4-tools-open-source-tool

## Change Log
- <strong>Version 1.0</strong>
	- Basic project renaming tool
-  <strong>Version 2.0</strong>
	-   Added C++ class renaming tool
	-   Added In application project backup
	-   Added a main menu
	-   New UI design
- <strong>Version 2.1</strong>
	- Fixed a bug where blueprints(which have user-defined C++ parents) become unlinked upon renaming a project or class
- <strong>Version 3.0</strong>
	- Added a tool for creating new c++ code modules.
- <strong>Version 4.0</strong>
	- Added a tool which generates new blueprint asset types with an accompanying factory class.
	- Added the groundwork for custom editor code within generated assets e.g. custom icons, colours, right-click actions, categories etc.
	-  Fixed crash while creating modules.
	- Fixed an issue where generated editor modules would prevent a game from building successfully.
	- "UnrealEd" is now automatically included in generated editor modules.
- <strong>Version 5.0</strong>
	- Added options for generating FAssetTypeActions_Base classes.
	- Fixed a crash when generating custom assets.
	- Minor UI size and layout tweaks.
- <strong>Version 5.1 (Current)</strong>
	- Fixed the include path of the module manager for newer versions of UE4.
	- Deprecated the module creation tool in favour of my module generator plugin. [Link](https://www.unrealengine.com/marketplace/en-US/product/ec36ec6e94f74075819e2a04f7cb97f3).
	-  Added support for public/private file structures when renaming classes.

