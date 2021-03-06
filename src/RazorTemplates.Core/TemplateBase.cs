﻿using System.Text;
using RazorTemplates.Core.Infrastructure;

namespace RazorTemplates.Core
{
    /// <summary>
    /// Represents a base class for generated templates.
    /// </summary>
    public abstract class TemplateBase
    {
        private readonly StringBuilder _buffer = new StringBuilder();

        /// <summary>
        /// Gets or sets dynamic model which data should be rendered.
        /// </summary>
        public virtual dynamic Model { get; set; }

        /// <summary>
        /// Renders specified model.
        /// </summary>
        public virtual string Render(object model)
        {
            this._buffer.Clear();

            this.Model = model;
            this.Execute();
            return _buffer.ToString();
        }

        /// <summary>
        /// A method which implemets by Razor engine.
        /// Produces sequance like:
        ///     WriteLiteral("Hello ");
        ///     Write(Model.Name);
        ///     WriteLiteral("!");
        /// </summary>
        public abstract void Execute();

        /// <summary>
        /// Writes a string.
        /// </summary>
        protected void Write(string value)
        {
            _buffer.Append(value);
        }

        /// <summary>
        /// Writes a string representation of specified object.
        /// </summary>
        protected void Write(object value)
        {
            _buffer.Append(value);
        }

        /// <summary>
        /// Writes specified string.
        /// </summary>
        protected void WriteLiteral(string value)
        {
            _buffer.Append(value);
        }

        /// <summary>
        /// Razor 2.0
        /// Writes attribute in situations like &lt;img src="@Model"&gt;.
        /// </summary>
        protected void WriteAttribute(
            string attribute,
            PositionTagged<string> prefix,
            PositionTagged<string> suffix,
            params AttributeValue[] values)
        {
            _buffer.Append(prefix.Value);

            if (values != null)
            {
                foreach (var attributeValue in values)
                {
                    _buffer.Append(attributeValue.Prefix.Value);

                    var value = attributeValue.Value.Value;
                    if (value != null)
                    {
                        _buffer.Append(value);
                    }
                }
            }

            _buffer.Append(suffix.Value);
        }
    }
}