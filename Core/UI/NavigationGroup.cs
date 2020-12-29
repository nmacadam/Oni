// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

namespace Oni.UI
{
	/// <summary>
	/// Creates a closed-system of selectable UI content; useful for seperating the content of stacking menu elements
	/// </summary>
	[DisallowMultipleComponent]
    [ExecuteAlways]
	public class NavigationGroup : UIBehaviour
	{
		public enum NavigationGroupDirection
		{
			Horizontal,
			Vertical,
			HorizontalLooping,
			VerticalLooping
		}

		public enum NavigationGroupElements
		{
			ImmediateChildren,
			AllChildren,
			Explicit
		}

		[Tooltip("In what direction should the user navigate to move between selectable content?")]
		[SerializeField] private NavigationGroupDirection _direction = NavigationGroupDirection.Horizontal;

		[Tooltip("Where should the group retrieve its elements from?")]
		[SerializeField] private NavigationGroupElements _groupElements = NavigationGroupElements.ImmediateChildren;
		[SerializeField] private List<Selectable> _content = new List<Selectable>();

        public NavigationGroupDirection Direction { get => _direction; set => _direction = value; }
        public List<Selectable> Content { get => _content; set => _content = value; }

        protected override void OnValidate() 
		{
			base.OnValidate();
			UpdateNavigation();
		}

		protected virtual void OnTransformChildrenChanged()
        {
			if (_groupElements != NavigationGroupElements.Explicit)
			{
            	UpdateNavigation();
			}
        }

		/// <summary> Iterates over the content elements and sets their navigation </summary>
		/// <remarks> This is called in editor automatically from OnValidate and OnTransformChildrenChanged (if using non-explicit content) </remarks>
		public void UpdateNavigation()
		{
			if (_groupElements == NavigationGroupElements.ImmediateChildren)
			{
				_content = GetSelectableChildren();
			}
			else if (_groupElements == NavigationGroupElements.AllChildren)
			{
				_content = GetComponentsInChildren<Selectable>().ToList();
			}

            SetLinearSelectableNavigation(_content);
		}

		private void SetLinearSelectableNavigation(List<Selectable> content)
		{
			if (content.Count == 0)
			{
				Debug.Log("No content");
				return;
			}
			else if (content.Count == 1)
			{
				Debug.Log("Only one element");

				Navigation navigation = content[0].navigation;
				navigation.mode = Navigation.Mode.Explicit;

				navigation.selectOnUp = null;
				navigation.selectOnDown = null;
				navigation.selectOnLeft = null;
				navigation.selectOnRight = null;

				content[0].navigation = navigation;

				return;
			}

			for (int i = 0; i < content.Count; i++)
			{
				Navigation navigation = content[i].navigation;
				navigation.mode = Navigation.Mode.Explicit;

				navigation.selectOnUp = null;
				navigation.selectOnDown = null;
				navigation.selectOnLeft = null;
				navigation.selectOnRight = null;

				if (i == 0)  // On first element, ignore previous or link to end of list
				{
					if (IsLooping())
					{
						Debug.Log("Setting loop");
						SetPrevious(ref navigation, content[content.Count - 1]);
					}

					SetNext(ref navigation, content[i + 1]);
				}
				else if (i == content.Count - 1)  // On last element, ignore next or link to start of list
				{
					if (IsLooping())
					{
						Debug.Log("Setting loop");
						SetNext(ref navigation, content[0]);
					}

					SetPrevious(ref navigation, content[i - 1]);
				}
				else  // Set navigation for previous and next to previous and next element in list
				{
					SetPrevious(ref navigation, content[i - 1]);
					SetNext(ref navigation, content[i + 1]);
				}
				
				content[i].navigation = navigation;
			}
		}

		private void SetPrevious(ref Navigation navigation, Selectable previous)
		{
			if (IsHorizontal())
			{
				navigation.selectOnLeft = previous;
			}
			if (IsVertical())
			{
				navigation.selectOnUp = previous;
			}
		}

		private void SetNext(ref Navigation navigation, Selectable next)
		{
			if (IsHorizontal())
			{
				navigation.selectOnRight = next;
			}
			if (IsVertical())
			{
				navigation.selectOnDown = next;
			}
		}

		private bool IsLooping()
		{
			return _direction == NavigationGroupDirection.HorizontalLooping || _direction == NavigationGroupDirection.VerticalLooping;
		}

		private bool IsHorizontal()
		{
			return _direction == NavigationGroupDirection.Horizontal || _direction == NavigationGroupDirection.HorizontalLooping;
		}

		private bool IsVertical()
		{
			return _direction == NavigationGroupDirection.Vertical || _direction == NavigationGroupDirection.VerticalLooping;
		}

		private List<Selectable> GetSelectableChildren()
		{
			List<Selectable> content = new List<Selectable>();

			for (int i = 0; i < transform.childCount; i++)
			{
				var child = transform.GetChild(i);
				var selectable = child.GetComponent<Selectable>();

				if (selectable != null && child.gameObject.activeSelf)
				{
					content.Add(selectable);
				}
			}

			return content;
		}
	}
}