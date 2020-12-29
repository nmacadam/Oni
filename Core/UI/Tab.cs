// Original work Copyright (c) 2019 Game Dev Guide
// CC-BY License
// https://www.youtube.com/watch?v=211t6r12XPQ

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Oni.UI
{
	/// <summary>
	/// 
	/// </summary>
	public class Tab : Selectable
	{
		[SerializeField] private TabGroup _tabGroup = default;
		[SerializeField] private UnityEvent _onTabSelected = default;
		[SerializeField] private UnityEvent _onTabDeselected = default;

        public TabGroup TabGroup { get => _tabGroup; set => _tabGroup = value; }
        public UnityEvent OnTabSelected { get => _onTabSelected; set => _onTabSelected = value; }
        public UnityEvent OnTabDeselected { get => _onTabDeselected; set => _onTabDeselected = value; }

        public override void OnSelect(BaseEventData eventData)
        {
			base.OnSelect(eventData);
            if(_tabGroup != null) _tabGroup.OnTabSelected(this);
			else Select();
        }

		public override void OnDeselect(BaseEventData eventData)
		{
			base.OnDeselect(eventData);
            if(_tabGroup != null) _tabGroup.OnTabExit(this);
			else Deselect();
		}

		public override void Select()
		{
			_onTabSelected.Invoke();
		}

		public void Deselect()
		{
			_onTabDeselected.Invoke();
		}

        protected override void Awake()
		{
			base.Awake();
			if(_tabGroup != null) _tabGroup.Subscribe(this);
		}
	}
}