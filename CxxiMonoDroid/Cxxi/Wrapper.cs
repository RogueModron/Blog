// -------------------------------------------------------------------------
//  Managed wrapper for Wrapper
//  Generated from Wrapper.g++_4_6_2.xml on 05/01/2012 12:22:32
//
//  This file was auto generated. Do not edit.
// -------------------------------------------------------------------------

using System;
using Mono.Cxxi;

namespace CxxiGeneratedNamespace.ClippingLibrary {
			public partial class Wrapper : ICppObject {

		private static readonly IWrapper impl = Libs.Native.GetClass<IWrapper,_Wrapper,Wrapper> ("Wrapper");
		public CppInstancePtr Native { get; protected set; }

		public static bool operator!=(Wrapper a, Wrapper b) { return !(a == b); }
		public static bool operator==(Wrapper a, Wrapper b)
		{
            if (object.ReferenceEquals(a, b))
                return true;
            if ((object)a == null || (object)b == null)
                return false;
			return a.Native == b.Native;
		}
        public override bool Equals(object obj) { return (this == obj as Wrapper); }
        public override int GetHashCode() { return this.Native.GetHashCode(); }

		[MangleAs ("class ClippingLibrary::Wrapper")]
		public partial interface IWrapper : ICppClassOverridable<Wrapper> {
			[Constructor] CppInstancePtr Wrapper (CppInstancePtr @this);
			[Destructor] void Destruct (CppInstancePtr @this);
			void AddInnerRing (CppInstancePtr @this);
			void AddOuterRing (CppInstancePtr @this);
			void AddPoint (CppInstancePtr @this, double x, double y);
			void ExecuteDifference (CppInstancePtr @this);
			void ExecuteIntersection (CppInstancePtr @this);
			void ExecuteUnion (CppInstancePtr @this);
			void ExecuteXor (CppInstancePtr @this);
			bool GetInnerRing (CppInstancePtr @this);
			bool GetOuterRing (CppInstancePtr @this);
			bool GetPoint (CppInstancePtr @this, [MangleAs ("double  &")] ref double x, [MangleAs ("double  &")] ref double y);
			void InitializeClip (CppInstancePtr @this);
			void InitializeSubject (CppInstancePtr @this);
		}
		public unsafe struct _Wrapper {
			public IntPtr _pImpl;
		}




		public Wrapper (CppTypeInfo subClass)
		{
			__cxxi_LayoutClass ();
			subClass.AddBase (impl.TypeInfo);
		}

		public Wrapper (CppInstancePtr native)
		{
			__cxxi_LayoutClass ();
			Native = native;
		}

		public Wrapper ()
		{
			__cxxi_LayoutClass ();
			Native = impl.Wrapper (impl.Alloc (this));
		}
		public void AddInnerRing ()
		{
			impl.AddInnerRing (Native);
		}
		public void AddOuterRing ()
		{
			impl.AddOuterRing (Native);
		}
		public void AddPoint (double x, double y)
		{
			impl.AddPoint (Native, x, y);
		}
		public void ExecuteDifference ()
		{
			impl.ExecuteDifference (Native);
		}
		public void ExecuteIntersection ()
		{
			impl.ExecuteIntersection (Native);
		}
		public void ExecuteUnion ()
		{
			impl.ExecuteUnion (Native);
		}
		public void ExecuteXor ()
		{
			impl.ExecuteXor (Native);
		}
		public bool GetInnerRing ()
		{
			return impl.GetInnerRing (Native);
		}
		public bool GetOuterRing ()
		{
			return impl.GetOuterRing (Native);
		}
		public bool GetPoint (ref double x, ref double y)
		{
			return impl.GetPoint (Native, ref x, ref y);
		}
		public void InitializeClip ()
		{
			impl.InitializeClip (Native);
		}
		public void InitializeSubject ()
		{
			impl.InitializeSubject (Native);
		}


		partial void BeforeDestruct ();
		partial void AfterDestruct ();

		public virtual void Dispose ()
		{
			BeforeDestruct ();
			impl.Destruct (Native);
			Native.Dispose ();
			AfterDestruct ();
		}

		private void __cxxi_LayoutClass ()
		{
			impl.TypeInfo.CompleteType ();
		}

	}
}

