using System;
using Core.Dialogues.StaticData;

namespace Core.Dialogues {
    public interface IDialogueService {
        
        bool IsFinishedTyping { get; }

        void FinishTyping();
        void SetDialogue(DialogueData dialogueData);
        void SetOptions(string[] options, Action<int> callback);
        void ClearText();
    }
}
