using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DevAge.Drawing.VisualElements
{
    [Serializable]
    public class ColumnHeaderThemed : ColumnHeaderBase
    {
        #region Constuctor
        /// <summary>
        /// Default constructor
        /// </summary>
        public ColumnHeaderThemed()
        {
        }

        /// <summary>
        /// Copy constructor
        /// </summary>
        /// <param name="other"></param>
        public ColumnHeaderThemed(ColumnHeaderThemed other)
            : base(other)
        {
        }
        #endregion
        /// <summary>
        /// Clone
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            return new ColumnHeaderThemed(this);
        }
        #region Properties
        /// <summary>
        /// Standard column header used when the XP style are disabled.
        /// </summary>
        private ColumnHeader mStandardHeader = new ColumnHeader();

        public override ControlDrawStyle Style
        {
            get{return base.Style;}
            set { base.Style = value; mStandardHeader.Style = value; }
        }
        #endregion

        #region Helper methods
        protected VisualStyleElement GetBackgroundElement()
        {
            if (Style == ControlDrawStyle.Hot)
                return VisualStyleElement.Header.Item.Hot;
            else if (Style == ControlDrawStyle.Pressed)
                return VisualStyleElement.Header.Item.Pressed;
            else 
                return VisualStyleElement.Header.Item.Normal;
        }

        /// <summary>
        /// Gets the System.Windows.Forms.VisualStyles.VisualStyleRenderer to draw the specified element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected System.Windows.Forms.VisualStyles.VisualStyleRenderer GetRenderer(VisualStyleElement element)
        {
            return new System.Windows.Forms.VisualStyles.VisualStyleRenderer(element);
        }
        #endregion

        #region Header
        protected override void OnDraw(GraphicsCache graphics, RectangleF area)
        {
            base.OnDraw(graphics, area);

            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(GetBackgroundElement()))
                GetRenderer(GetBackgroundElement()).DrawBackground(graphics.Graphics, Rectangle.Round(area));
            else
                mStandardHeader.Draw(graphics, area);
        }

        public override RectangleF GetBackgroundContentRectangle(MeasureHelper measure, RectangleF backGroundArea)
        {
            //First calls the base class for standard calculation
            backGroundArea = base.GetBackgroundContentRectangle(measure, backGroundArea);

            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(GetBackgroundElement()))
                return GetRenderer(GetBackgroundElement()).GetBackgroundContentRectangle(measure.Graphics, Rectangle.Round(backGroundArea));
            else
                return mStandardHeader.GetBackgroundContentRectangle(measure, backGroundArea);
        }

        public override SizeF GetBackgroundExtent(MeasureHelper measure, SizeF contentSize)
        {
            if (Application.RenderWithVisualStyles && VisualStyleRenderer.IsElementDefined(GetBackgroundElement()))
            {
                Rectangle content = new Rectangle(new Point(0, 0), Size.Ceiling(contentSize));
                contentSize = GetRenderer(GetBackgroundElement()).GetBackgroundExtent(measure.Graphics, content).Size;
            }
            else
                contentSize = mStandardHeader.GetBackgroundExtent(measure, contentSize);

            //Calls the base class for standard calculation
            return base.GetBackgroundExtent(measure, contentSize);
        }

        #endregion
    }
}
