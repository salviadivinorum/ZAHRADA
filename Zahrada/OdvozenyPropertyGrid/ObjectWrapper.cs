using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Zahrada.OdvozenyPropertyGrid
{
	/// <summary>
	/// This class is a wrapper. It contains the object the propertyGrid has to display.
	/// Article on CodeProject : http://www.codeproject.com/Articles/13342/Filtering-properties-in-a-PropertyGrid
	/// </summary>
	internal class ObjectWrapper : ICustomTypeDescriptor
	{
		/// <summary>Contain a reference to the selected objet that will linked to the parent PropertyGrid.</summary>
		private object m_SelectedObject = null;
		
		/// <summary>Contain a reference to the collection of properties to show in the parent PropertyGrid.</summary>
		/// <remarks>By default, m_PropertyDescriptors contain all the properties of the object. </remarks>
		List<PropertyDescriptor> m_PropertyDescriptors = new List<PropertyDescriptor>();

		/// <summary>Simple constructor.</summary>
		/// <param name="obj">A reference to the selected object that will linked to the parent PropertyGrid.</param>
		internal ObjectWrapper(object obj)
		{
			m_SelectedObject = obj;
		}

		/// <summary>Get or set a reference to the selected objet that will linked to the parent PropertyGrid.</summary>
		public object SelectedObject
		{
			get { return m_SelectedObject; }
			set { if (m_SelectedObject != value) m_SelectedObject = value; }
		}

		/// <summary>Get or set a reference to the collection of properties to show in the parent PropertyGrid.</summary>
		public List<PropertyDescriptor> PropertyDescriptors
		{
			get { return m_PropertyDescriptors; }
			set { m_PropertyDescriptors = value; }
		}

		#region ICustomTypeDescriptor Members        
		/// <summary>
		/// Returns the properties for this instance of a component using the attribute array as a filter.
		/// </summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the filtered properties for this component instance.</returns>
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			return GetProperties();
		}

		/// <summary>
		/// Returns the properties for this instance of a component.
		/// </summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties for this component instance.</returns>
		public PropertyDescriptorCollection GetProperties()
		{
			return new PropertyDescriptorCollection(m_PropertyDescriptors.ToArray(), true);
		}

		/// <summary>GetAttributes.</summary>
		/// <returns>AttributeCollection</returns>
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(m_SelectedObject, true);
		}
		/// <summary>Get Class Name.</summary>
		/// <returns>String</returns>
		public String GetClassName()
		{
			return TypeDescriptor.GetClassName(m_SelectedObject, true);
		}
		/// <summary>GetComponentName.</summary>
		/// <returns>String</returns>
		public String GetComponentName()
		{
			return TypeDescriptor.GetComponentName(m_SelectedObject, true);
		}

		/// <summary>GetConverter.</summary>
		/// <returns>TypeConverter</returns>
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(m_SelectedObject, true);
		}

		/// <summary>GetDefaultEvent.</summary>
		/// <returns>EventDescriptor</returns>
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(m_SelectedObject, true);
		}

		/// <summary>GetDefaultProperty.</summary>
		/// <returns>PropertyDescriptor</returns>
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(m_SelectedObject, true);
		}

		/// <summary>GetEditor.</summary>
		/// <param name="editorBaseType">editorBaseType</param>
		/// <returns>object</returns>
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		/// <summary>
		/// Returns the events for this instance of a component using the specified attribute array as a filter.
		/// </summary>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that is used as a filter.</param>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the filtered events for this component instance.</returns>
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(m_SelectedObject, attributes, true);
		}

		/// <summary>
		/// Returns the events for this instance of a component.
		/// </summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> that represents the events for this component instance.</returns>
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(m_SelectedObject, true);
		}

		/// <summary>
		/// Returns an object that contains the property described by the specified property descriptor.
		/// </summary>
		/// <param name="pd">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that represents the property whose owner is to be found.</param>
		/// <returns>An <see cref="T:System.Object" /> that represents the owner of the specified property.</returns>
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return m_SelectedObject;
		}

		#endregion

	}
}
