using System;

namespace Uniject {
    public interface IButton {
		void AddListener(IButtonListener listener);
		void RemoveListener(IButtonListener listener);
    }
}

