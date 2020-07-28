![Dev Companion Logo](DevCompanion.Desktop/Logo_300.png)
# Dev Companion
This project is a desktop application that will give developers a tool to greatly reduce the amount of time needed to configure and deploy locally hosted cloud apps.

A key feature of Dev Companion is the ability to allow teams and developers to create and share onboarding helpers to help new team members get setup quickly, and help assure everyone stays in sync as project configurations are changed.

## Intended launch features include:
- Onboarding workflow with step-by-step guidance for developers to setup their local environment for deploying projects locally (websites, fabric cluster, etc). 
- Sharing scripts (Powershell, command prompt).
- Script templating allowing variables editable in UI 
- Easily update local environment variables as well as Azure App Config services that your local projects connect to.
- Sharing team & project documentation (Architecture, features, coding standards, etc) organized using a tag-based system inspired by mind-map applications.
- Health report that will verify required SDKs/configurations are installed/setup as expected for Team/project.
- Event driven triggers to run specified scripts on specified events (file updates, button in UI, git commit, etc).
- Configuration (Blueprint) information is encrypted and stored securily using Azure Storage.

## Software Expectations
- Dev Companion uses the naming convention of "Blueprints" to represent a collection of configurations, documentation, scripts, and solutions.
- Dev Companion can track multiple blueprints to be loaded and saved as desired.
- Only 1 blueprint can be loaded at a time.
- Users will have the option of storing Blueprints either locally or in the Cloud at the time of creation.
- Blueprints cannot have their storage location changed between being stored locally or in the Cloud.
- Blueprints can be copied via a Save-As feature.
- Encryption keys are never passed to or stored in the Cloud. Blueprints are never decrypted in the Cloud.
- Blueprint Cloud URLs and Encryption keys can be stored and loaded from Azure Key Vaults, to help assure security by teams not needing to pass around these secrets. Users will only need to have appropriate permissions to allow them to read the key vault secrets from a PowerShell script, which the application will use internally to load the values to access the Blueprint(s) with.
- Blueprints are never saved to disk unencrypted. 
- Dev Companion will include app insights logging to log anonymous usage patterns and bugs. These will be disabled by default, only enabling when the user opts-in for them, and will never send Blueprint details, user information, or any other information that could be deemed sensitive information. The sole purpose of logging is to help gauge what features of the app are used most and least, and track bugs so the software can be fixed and improved.

## Expected Workflows

### Opening Dev Companion for the first time.
When opening the application for the first time, the user should be presented with a startup guide giving them a quick overview of the application. Any Blueprint specific menu items should be disabled.

### Setting URL for Cloud Storage API Endpoint
To enable Cloud Storage, user will need to update setting.
- User will set Cloud Storage API URL and License fields in Settings -> Cloud -> *.
- Dev Companion will validate syntax and then ping URL to confirm valid response.
- When valid, API URL will be saved to app registry for Dev Companion to load settings from on subsequent loads.

### Creating a new Blueprint.
Blueprints are displayed as a list of units or steps.
- When creating/updating, the user can add, remove, and update units.
- Users will define each unit with a combination of setup instructions and rules pertaining to the step and how the application is expeted to verify.
- Users can also run units individually against their own machine to verify the rules against their machine. Units are not required to pass, as it is expected that sometimes these rules may be defined and created before any local deployments have been made.
- Blueprints will need to be given a user friendly name.
- Users will have the option of saving Blueprints they create either locally on their machine or in the Cloud.
- When saved for the first time, Blueprints will be saved with an newly created random encryption key, regardless of whether saved locally or in the Cloud.
- When saved to the cloud, a URL will be created that the user can share to their team to access the Blueprint. Both the URL and encryption key will be required to access and decrypt the Blueprint.
- If a user has an existing Blueprint, they can also use a Save-As feature to create a new Blueprint using the values from the current Blueprint. This will copy over all units, and their local settings automatically applied, but will create a new encryption key for the new Blueprint.

### Opening an existing Blueprint.
When loading an existing Blueprint, users will need to have the encryption key shared with them, as well as access to the Blueprint itself.
- Locally saved Blueprint files can be shared the same as any file. Once received, the user simply needs to place the file in an appropriate folder and open the Blueprint from within Dev Companion. When opening, they will be asked to enter the encryption key, or key vault url to retrieve the encryption key from.
- Cloud saved Blueprints can be opened when the user has been given the URL and encryption key. Both will be required to enter. The user can enter either manually, or provide an Azure Key Vault URL for each respective secret containing the URL and encryption key.

### Changing Application Settings.
- Blueprints
  - Enable auto-syncing when changes are detected to blueprint configurations.

### Changing Loaded Blueprint.
When the user has a blueprint loaded, they can switch to a different Blueprint, either by creating a new one, or loading a different Blueprint.
- Only 1 Blueprint will be loaded at a time.
- A top menu Blueprint item will store a list of the last 10 unique Blueprints they had opened. This will allow quicker access to recently accessed Blueprints.

### Running Health Report to validate local configurations.
This service will cycle through a Blueprint configuration and display all the units as a list of steps.
- Only Unit Validators are run during Health Reports
- The user can opt to run all health reports, or trigger health checkups for speciic Units.
- When a unit passes it will be displayed with a green checkmark icon next to it.
- When a unit fails and it is required then it will be displayed with a red X icon next to it. 
- When a unit fails but is not required then it will be displayed with an orange exclamation icon next to it.
- Any unit can be clicked to open a helper instructing the user what they need to do in order to configure their machine to get the unit passing.

### Setting Event Triggers
This service will allow users to define an event to watch for that will trigger a specific Unit (Script, Workflow, Etc).

## Blueprint Units
### Summary
- Blueprint units can represent a variety of types as detailed below.
- Each unit consists of 2 stages, a [Processor], and a [Validator].
	- Processor: This stage represents a process that needs to be viewed or run to complete the expected task of the unit.
    - Validator: This stage represents a process that needs to be run after the Processor and during Health checkups to validate the unit success conditions have been met. Depending on the unit type this may need to be defined with a specific script from the user, or may be automatically set such as in the case of documentation types, which will automatically have an associated acknowledgment flag and a Validator that checks if the user has acknowledged the content, setting the flag to true.
- Any value can be a static text value, a reference to a different unit containing a static value, or a url for a key vault key/secret/certificate value.
- If no value is given, then the user is expected to provide one.
- Users always have the option to overwrite Blueprint provided values with their own locally defined values, without updating the actual Blueprint definitions.
- Each Unit specified for a Blueprint must include a detailed description explaing to users things like what the variable is for, how and when it is used, and what format they are expected to enter data.
- Unit definitions can include value rules to assure users enter values meeting required specifications. This could include requiring a value to be a date with a specific format, a GUID, an integer that falls within a given range, or a number of other rules that will be expanded on in later stages of development.
- Units can be flagged as Required or Optional. Required units must be met before a Blueprint is considered passing.
- Units need to be flagged with purpose to clarify when they need to be used. Expected to generally be used for Workflow units, but can also be checked for other unit types that need acknowledgment or are part of global configurations/deployments. Thinking units must always be flagged with 1 and only 1 flag. May change this if use cases come up that show a need to allow multiple flags.
	- [Configuration] - Flag the unit is a requirement passing configuration health.
    - [Deployment] - Flag the unit is part of deployment, meaning it will be run during specific deployments
	- [Documentation] - A unit is included as documentation. Units that perform processing but are flagged as documentation can be run on demand, or included as steps in Workflows.

### Specific Unit Types
- Environment Variable [Machine|User]: Set name w/ optional value
- Azure Key Vault Variable
- Blueprint: Allow creating micro-blueprints for specific sub-projects. Use case for when this is needed/desired, is having a Blueprint for a team, but micro-blueprints for individual projects within that team. Where indivual developers each may have separate projects they are working on, and want to make sure they are configured for their specific projects, but don't want to concern themselves with getting setup on other projects they aren't directly working on. Blueprint units could be set as options, with the mini-blueprint itself having required units which are only required when user flags it as active.
- PowerShell script: Scripts that need to be run in PowerShell.
	- Name: Give name for script
    - Prereq: List other units that need to be loaded/passing 1st - useful for ensuring required environment variables are ready, run other scripts to set variables or declare functions, etc.
	- Variables: Define input variables - Autopopulate based on script content
    - Result: String value to check output at end of run to determine unit passes.
- Command Prompt Scripts: Similar setup as PowerShell, but for Command Prompt
- Documentation: Static content that can be included to require acknowledgment by user to consider passing. Any time documentation is updated and synced, acknowledgment will be reset so team members will know their is updated information.
- Workflow - A specific series of processing steps to accomplish some task. Workflow Steps can include other Workflow's as long as no recursive looping results.


## Other Planned Potential Features for the Future
- Online sharing of scripts to be accessible by any users.
	- Users would be able to browse and search within Dev Companion to select and import sripts into their Blueprints.
	- Users would be able to upload their own scripts to be shared.
	- Scripts will be validated against to assure secrets are not included.
    - Online web tool will allow user to search/browse for scripts as well, and use an online editor to populate a script with their desired variable values so they can copy/paste the complete script into their local script tool.
