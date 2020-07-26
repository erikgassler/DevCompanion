# Dev Companion

This project is a desktop application that will give developers a tool to greatly reduce the amount of time needed to configure and deploy locally hosted cloud apps.

A key feature of Dev Companion is the ability to allow teams and developers to create and share onboarding helpers to help new team members get setup quickly, and help assure everyone stays in sync as project configurations are changed.

## Intended features include:
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
- Dev Companion will include app insights logging to log anonymous usage patterns and bugs. These will be disabled by default, only enabling when the user opts-in for them, and will never send Blueprint details, user information, or any other information that could be deemed sensitive information. The sole purpose of logging is to help gauge what features of the app are used most and least, and track bugs so the software can be fixed and improved.

## Expected Workflows

### Opening Dev Companion for the first time.
When opening the application for the first time, the user should be presented with a startup guide giving them a quick overview of the application. Any Blueprint specific menu items should be disabled.

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

### Changing Application Settings
- Blueprints
  - Enable auto-syncing when changes are detected to blueprint configurations.

### Running Health Report to validate local configurations.
This service will cycle through a Blueprint configuration and display all the units as a list of steps.
- When a unit passes it will be displayed with a green checkmark next to it.
- When a unit fails it will be displayed with a red X next to it. 
- Any unit can be clicked to open a helper instructing the user what they need to do in order to configure their machine to get the unit passing.

## Blueprint Units
### Summary
- Blueprint units can represent a variety of types as detailed below.
- Any value can be a static text value, a reference to a different unit containing a static value, or a url for a key vault key/secret/certificate value.
- If no value is given, then the user is expected to provide one.
- Users always have the option to overwrite Blueprint provided values with their own locally defined values, without updating the actual Blueprint definitions.
- Each Unit specified for a Blueprint must include a detailed description explaing to users things like what the variable is for, how and when it is used, and what format they are expected to enter data.
- Unit definitions can include value rules to assure users enter values meeting required specifications. This could include requiring a value to be a date with a specific format, a GUID, an integer that falls within a given range, or a number of other rules that will be expanded on in later stages of development.

### Specific Unit Types
- Environment Variable [Machine|User]: Set name w/ optional value
- Azure Key Vault Connection String
- Azure Key Vault Variable

