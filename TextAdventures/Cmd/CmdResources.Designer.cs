﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TextAdventures.Cmd {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class CmdResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CmdResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TextAdventures.Cmd.CmdResources", typeof(CmdResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Congratulations ! You have completed the adventure, try using clear to reset back to the menu :) !.
        /// </summary>
        internal static string EndGame {
            get {
                return ResourceManager.GetString("EndGame", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You need to start a game to before you can use this command !.
        /// </summary>
        internal static string GameNotStarted {
            get {
                return ResourceManager.GetString("GameNotStarted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Menu commands
        ///
        ///Help - Shows commands available to you
        ///Clear - Clears the screen and resets
        ///Select [Mode: 1/2] - 1 = New game, 2 = Load game - All commands after this are mode specific
        ///Show - Shows all games/saves available
        ///Start [Game name] - Starts the game/save
        ///
        ///Game commands
        ///
        ///Go to [Room name] - Moves your character to the next room - Ex. Go to kitchen
        ///Get [Item name] from [Room object name] - Your character picks up an item from a room object and places it in your inventory
        ///Use [Item name] o [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string Help {
            get {
                return ResourceManager.GetString("Help", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The provided arguments are not valid for this command !.
        /// </summary>
        internal static string InvalidArgs {
            get {
                return ResourceManager.GetString("InvalidArgs", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Missing or too many arguments !.
        /// </summary>
        internal static string InvalidLength {
            get {
                return ResourceManager.GetString("InvalidLength", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You can&apos;t use this command with your selected mode !.
        /// </summary>
        internal static string InvalidMode {
            get {
                return ResourceManager.GetString("InvalidMode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Welcome to Text Adventures !
        ///You navigate through commands, try typing &quot;help&quot; then enter to get commands available to you :).
        /// </summary>
        internal static string Welcome {
            get {
                return ResourceManager.GetString("Welcome", resourceCulture);
            }
        }
    }
}
