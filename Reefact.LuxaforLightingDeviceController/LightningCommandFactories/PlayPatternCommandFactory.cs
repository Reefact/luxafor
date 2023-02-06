﻿#region Usings declarations

using System;
using System.ComponentModel;

#endregion

namespace Reefact.LuxaforLightingDeviceController.LightingCommandFactories {

    internal sealed class PlayPatternCommandFactory : LightingCommandFactory {

        #region Fields declarations

        private readonly BuiltInPattern _pattern;
        private readonly Repeat         _repeat;

        #endregion

        #region Constructors declarations

        public PlayPatternCommandFactory(BuiltInPattern pattern, Repeat repeat) {
            if (repeat is null) { throw new ArgumentNullException(nameof(repeat)); }
            if (!Enum.IsDefined(typeof(BuiltInPattern), pattern)) { throw new InvalidEnumArgumentException(nameof(pattern), (int)pattern, typeof(BuiltInPattern)); }

            _pattern = pattern;
            _repeat  = repeat;
        }

        #endregion

        /// <inheritdoc />
        public LightingCommand Create() {
            CommandMode     mode                 = CommandMode.From(_pattern);
            BrightColor     color                = new BrightColor(_repeat.ToByte(), 0, 0);
            string          stringRepresentation = CreateStringRepresentation();
            LightingCommand command              = new LightingCommand(CommandCode.ActivateBuiltInPatterns, mode, color, Option.UnUsed, Option.UnUsed, Option.UnUsed, stringRepresentation);

            return command;
        }

        private string CreateStringRepresentation() {
            return $"Play a built-in pattern {_pattern} {_repeat}.";
        }

    }

}