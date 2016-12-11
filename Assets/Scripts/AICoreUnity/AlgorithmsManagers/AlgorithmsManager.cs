using UnityEngine;

namespace AICoreUnity {
    public interface AlgorithmsManager {
        void ManageAI(MovementAI ai, Rigidbody2D character);
        void ManageEditor(MovementAIEditor editor);
    }
}