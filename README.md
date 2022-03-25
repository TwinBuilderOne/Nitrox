# Plugins

Plugins are modifications to the game that allow for additional features and experiences. They are created by the server, sent to the client when they join, and are dynamically loaded and integrated into the game.

## Isn't that a huge security problem?

Well, yes. The server could theoretically take control of the client's machine and cause all sorts of problems. Be careful joining servers with this version of Nitrox, and only join servers you know to be safe.

## Why can't I just use the main version of Nitrox?

While having plugins be server-side only is ideal, this is sadly impossible. Unlike other games where servers control most aspects of the game, the Nitrox server is only in charge of synching information between players and keeping track of world state. Therefore, plugins must also be able to modify the client to get around the server's limited role. This sort of functionality is not included by default and requires a pre-modified client to load and execute plugins.

# Disclaimer

When you download and use this version of Nitrox, you do so at your own risk. I am not responsible for any event resulting from use of this version.

# Plugin API (for server owners)

Plugins should be in the form of a dll file named `Plugin.dll` in the same folder as `NitroxServer-Subnautica.exe`. Each server can only have one plugin file. Plugin assemblies should have a public static class named `Plugin` with a public static void method named `Initialize` with no parameters. This method is called when the plugin is loaded, which is when the client first connects to the server. This is where patches should be applied and events should be hooked. Additionally, the `Plugin` class can also have a public static parameterless void method named `Destroy`, which is called when the plugin is unloaded (e.g., when the client exits the Join Server menu).