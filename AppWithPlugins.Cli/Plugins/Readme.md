# Plugins folder

Put the plugins in their respective directories here.
> Plugins are the output of the class library build

Example:
```
Plugins
│    Readme.md
│
└─── AppWithPlugins.Plugin.Adder
│    └─── AppWithPlugins.Plugin.Adder.deps.json
│    └─── AppWithPlugins.Plugin.Adder.dll
│    └─── AppWithPlugins.Plugin.Adder.pdb
│    └─── AppWithPlugins.Plugins.Contracts.dll
│    └─── AppWithPlugins.Plugins.Contracts.pdb
│   
└─── AppWithPlugins.Plugin.Subtractor
|    └─── AppWithPlugins.Plugin.Subtractor.dll
|   
└─── AppWithPlugins.Plugin.Multiplier
|    └─── AppWithPlugins.Plugin.Multiplier.dll
|   
└─── AppWithPlugins.Plugin.Divider
|    └─── AppWithPlugins.Plugin.Divider.dll
|   
└─── Any.other.plugin.name
     └─── Any.other.plugin.name.dll
```

You can put all the output, but only the project dll is needed.
