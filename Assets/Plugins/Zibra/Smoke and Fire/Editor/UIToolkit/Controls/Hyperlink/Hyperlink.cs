﻿#if UNITY_2019_4_OR_NEWER
using com.zibraai.smoke_and_fire.Foundation.Editor;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;

namespace com.zibraai.smoke_and_fire.Foundation.UIElements
{
    internal class Hyperlink : BindableElement
    {
        [UsedImplicitly]
        public new class UxmlFactory : UxmlFactory<Hyperlink, UxmlTraits>
        {
        }

        public new class UxmlTraits : BindableElement.UxmlTraits
        {
            readonly UxmlStringAttributeDescription m_Link = new UxmlStringAttributeDescription { name = "link" };

            public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
            {
                base.Init(ve, bag, cc);
                ((Hyperlink)ve).Link = m_Link.GetValueFromBag(bag, cc);
            }
        }

        public const string USSClassName = "zibraai-hyperlink-block";
        public const string HeaderUssClassName = USSClassName + "__header";
        public const string ContentUssClassName = USSClassName + "__content";

        string m_Link;
        readonly VisualElement m_Container;

        public override VisualElement contentContainer => m_Container;

        public string Link
        {
            get => m_Link;
            set => m_Link = value;
        }

        public Hyperlink()
        {
            AddToClassList(USSClassName);

            var button = new Button()
            {
                name = "header",
            };
            button.clicked += () =>
            { Application.OpenURL(m_Link); };
            button.AddToClassList(HeaderUssClassName);

            m_Container = new VisualElement()
            {
                name = "content",
            };
            m_Container.AddToClassList(ContentUssClassName);

            button.Add(m_Container);

            hierarchy.Add(button);

            UIToolkitEditorUtility.ApplyStyleForInternalControl(this, nameof(Hyperlink));
        }
    }
}
#endif