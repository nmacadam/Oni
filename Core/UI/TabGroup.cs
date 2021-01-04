// Original work Copyright (c) 2019 Game Dev Guide
// CC-BY License
// https://www.youtube.com/watch?v=211t6r12XPQ

// Modified for ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Oni.UI
{
	/// <summary>
	/// 
	/// </summary>
	public class TabGroup : UIBehaviour
	{
		[SerializeField] private Tab _selectedTab;

		private List<Tab> tabs = new List<Tab>();

		public void Subscribe(Tab tab)
		{
			tabs.Add(tab);
		}

		public void OnTabEnter(Tab tab)
		{
			ResetTabs();
			if (_selectedTab == null || tab != _selectedTab)
			{
				tab.targetGraphic.color = tab.colors.highlightedColor;
			}
		}

		public void OnTabExit(Tab tab)
		{
			ResetTabs();
		}

		public void OnTabSelected(Tab tab)
		{
			if (_selectedTab != null)
			{
				_selectedTab.Deselect();
			}

			_selectedTab = tab;
			_selectedTab.Select();

			ResetTabs();
			tab.targetGraphic.color = tab.colors.selectedColor;
		}

		public void ResetTabs()
		{
			foreach (var tab in tabs)
			{
				if (_selectedTab != null && tab == _selectedTab)
				{
					continue;
				}

				tab.targetGraphic.color = tab.colors.normalColor;
			}
		}

		public void ClearSelection()
		{
			foreach (var tab in tabs)
			{
				if (_selectedTab != null && tab == _selectedTab)
				{
					tab.Deselect();
				}
				tab.targetGraphic.color = tab.colors.normalColor;
			}
		}

		protected override void Start()
		{
			base.Start();
			
			if (_selectedTab != null)
			{
				_selectedTab.targetGraphic.color = _selectedTab.colors.selectedColor;
			}
		}
	}
}