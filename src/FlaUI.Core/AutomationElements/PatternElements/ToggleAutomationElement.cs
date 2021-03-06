﻿using System;
using FlaUI.Core.AutomationElements.Infrastructure;
using FlaUI.Core.Definitions;
using FlaUI.Core.Patterns;

namespace FlaUI.Core.AutomationElements.PatternElements
{
    /// <summary>
    /// Class for an element that supports the <see cref="ITogglePattern"/>.
    /// </summary>
    public class ToggleAutomationElement : AutomationElement
    {
        /// <summary>
        /// Creates an element with a <see cref="ITogglePattern"/>.
        /// </summary>
        public ToggleAutomationElement(BasicAutomationElementBase basicAutomationElement) : base(basicAutomationElement)
        {
        }

        /// <summary>
        /// The toggle pattern.
        /// </summary>
        public ITogglePattern TogglePattern => Patterns.Toggle.Pattern;

        /// <summary>
        /// Gets or sets the current toggle state.
        /// </summary>
        public ToggleState ToggleState
        {
            get => TogglePattern.ToggleState.Value;
            set
            {
                // Loop for all states
                for (var i = 0; i < Enum.GetNames(typeof(ToggleState)).Length; i++)
                {
                    // Break if we're in the correct state
                    if (ToggleState == value) return;
                    // Toggle to the next state
                    Toggle();
                }
            }
        }

        /// <summary>
        /// Gets or sets if the element is toggled.
        /// </summary>
        public bool? IsToggled
        {
            get => ToggleStateToBool(ToggleState);
            set
            {
                if (IsToggled == value)
                {
                    return;
                }
                ToggleState = BoolToToggleState(value);
            }
        }

        /// <summary>
        /// Toggles the element.
        /// </summary>
        public virtual void Toggle()
        {
            TogglePattern.Toggle();
        }

        private bool? ToggleStateToBool(ToggleState toggleState)
        {
            switch (toggleState)
            {
                case ToggleState.Off:
                    return false;
                case ToggleState.On:
                    return true;
                case ToggleState.Indeterminate:
                    return null;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toggleState));
            }
        }

        private ToggleState BoolToToggleState(bool? toggled)
        {
            switch (toggled)
            {
                case false:
                    return ToggleState.Off;
                case true:
                    return ToggleState.On;
                case null:
                    return ToggleState.Indeterminate;
                default:
                    throw new ArgumentOutOfRangeException(nameof(toggled));
            }
        }
    }
}
