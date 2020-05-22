# UE4 Tools
UE4 tools is a set of tools created to perform several tedious tasks related to UE4 development.

<strong>Current Features</strong>
<ul>
	<li>Fully Rename a C++ Project</li>
	<li>Rename a C++ class and all references</li>
    <li>Generate new C++ module</li>
    <li>Generate custom Assets with factory classes</li>
	<li>Backup project files from inside the application</li>
</ul>

## Planned Features
<ul>
    <li>Generate editor code for custom assets e.g. categories, tooltips, custom context actions(right-click actions), colours and icons.</li>
</ul>

## Download Versions
Built Program Download: https://samcarey.itch.io/ue4-tools-open-source-tool

## Change Log
<ul>
	<li><strong>Version 1.0</strong></li>
	<ul>
		<li>Basic project renaming tool</li>
	</ul>
	<li><strong>Version 2.0</strong></li>
	<ul>
		<li>Added C++ class renaming tool</li>
		<li>Added In application project backup</li>
		<li>Added a main menu</li>
		<li>New UI design</li>
	</ul>
	<li><strong>Version 2.1 (Bug Fixes)</strong></li>
	<ul>
		<li>Fixed a bug where blueprints(which have user-defined C++ parents) become unlinked upon renaming a project or class</li>
	</ul>
    <li><strong>Version 3.0 </strong></li>
	<ul>
		<li>Added a tool for creating new c++ code modules</li>
	</ul>
        <li><strong>Version 4.0 </strong></li>
	<ul>
		<li>Added a tool which generates new blueprint asset types with an accompanying factory class.</li>
        <li>Added the groundwork for custom editor code within generated assets e.g. custom icons, colours, right-click actions, categories etc.</li>
        <li>Fixed crash while creating modules.</li>
        <li>Fixed an issue where generated editor modules would prevent a game from building successfully.</li>
        <li>"UnrealEd" is now automatically included in generated editor modules.</li>
	</ul>
</ul>
