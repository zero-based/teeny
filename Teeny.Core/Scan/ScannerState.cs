using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Teeny.Core.Scan.Attributes;

namespace Teeny.Core.Scan
{
    public class ScannerState
    {
        private State _state = State.Unknown;

        public ScannerState()
        {
            BuildLookupTables();
        }

        private Dictionary<State, RegularStateAttribute> RegularStatesLookup { get; } = new Dictionary<State, RegularStateAttribute>();
        private Dictionary<State, StreamStateAttribute> StreamStatesLookup { get; } = new Dictionary<State, StreamStateAttribute>();
        private StringBuilder LexemeBuilder { get; } = new StringBuilder();

        public State State
        {
            get => _state;
            set
            {
                if (_state == value) return;

                // Notify for change if there's a lexeme to notify for
                if (LexemeBuilder.Length > 0) OnStateChanged(LexemeBuilder);

                // Update type and start a new lexeme
                _state = value;
                LexemeBuilder.Clear();
            }
        }

        public Action<StringBuilder> OnStateChanged { get; set; }

        public void Update(ScanFrame frame)
        {
            if (StreamStatesLookup.ContainsKey(State))
            {
                LexemeBuilder.Append(frame.Center);

                // Close stream if it's eligible for closure
                var streamAttribute = StreamStatesLookup[State];
                if (frame.Matches(streamAttribute.ClosePattern))
                    State = State.StreamClosed;

                return;
            }

            State = GetState(frame);
            LexemeBuilder.Append(frame.Center);
        }

        private State GetState(ScanFrame frame)
        {
            foreach (var (streamState, streamAttribute) in StreamStatesLookup)
                if (frame.Matches(streamAttribute.OpenPattern))
                    return streamState;

            foreach (var (regularState, regularAttribute) in RegularStatesLookup)
                if (frame.CenterMatches(regularAttribute.Pattern))
                    return regularState;

            return State.Unknown;
        }

        private void BuildLookupTables()
        {
            var states = Enum.GetValues(typeof(State)).Cast<State>();
            foreach (var state in states)
            {
                var regularAttribute = state.GetAttributeOfType<RegularStateAttribute>();
                if (regularAttribute != null) RegularStatesLookup.Add(state, regularAttribute);

                var streamAttribute = state.GetAttributeOfType<StreamStateAttribute>();
                if (streamAttribute != null) StreamStatesLookup.Add(state, streamAttribute);
            }
        }
    }
}