#pragma once

using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

#include <myo/libmyo.h>
#include <vector>
#include "IHub.hpp"
#include "MyoEventArgs.hpp"
#include "RecognizedArmEventArgs.hpp"
#include "Myo.hpp"

#include "IHub.hpp"

namespace MyoNet
{
	namespace Myo
	{
		/// <summary>A Hub provides access to one or more Myo instances.</summary>
		public ref class Hub : public IHub
		{
		private:
			libmyo_hub_t _hub;
			System::Collections::Generic::List<IMyo^>^ _myos;

			LockingPolicy _lockPolicy;	// stored because libmyo does not provide a way to retrieve 
										// the current locking policy.

		internal:
			IMyo^ _AdoptMyo(libmyo_myo_t opaqueMyo);
			IMyo^ _FindMyo(libmyo_myo_t opaqueMyo);

			void _OnDeviceEvent(libmyo_event_t ev);
			libmyo_hub_t _libmyoObject( ) { return _hub; }

			property System::Collections::Generic::IList<IMyo^>^ Myos { System::Collections::Generic::IList<IMyo^>^ get( ) { return _myos; } }

		public:
			
			/// <summary>
			/// Gets or sets the locking policy for Myos connected to the Hub.
			/// </summary>
			virtual property LockingPolicy LockingPolicy 
			{ 
				::MyoNet::Myo::LockingPolicy get();
				void set(::MyoNet::Myo::LockingPolicy);
			}

			/// <summary>
			/// Initializes a new instance of <see cref="Hub"/>
			/// </summary>
			Hub( );

			/// <summary>
			/// Initializes a new instance of <see cref="Hub"/>
			/// </summary>
			/// <param name="applicationIdentifier">The application identifier.</param>
			Hub(String^ applicationIdentifier);

			/// <summary>
			/// Deallocate any resources associated with a Hub.
			/// </summary>
			~Hub( );

			/// <summary>
			/// Wait for a Myo to become paired.
			/// </summary>
			virtual IMyo^ WaitForMyo( );

			/// <summary>
			/// Wait for a Myo to become paired, or time out after <paramref name="timeout"/>. 
			/// </summary>
			/// <param name="timeout">The amount of time to wait until time out occurs.</param>
			virtual IMyo^ WaitForMyo(TimeSpan timeout);

#if defined NETFX_40
			[System::ComponentModel::EditorBrowsableAttribute(System::ComponentModel::EditorBrowsableState::Never)]
			virtual System::Threading::Tasks::Task<IMyo^>^ WaitForMyoAsync(TimeSpan timeout); // not implemented.
#endif

			/// <summary>
			/// Run the event loop.
			/// </summary>
			virtual void Run( );

			/// <summary>
			/// Run the event loop until a single event occurs. 
			/// </summary>
			virtual void RunOnce( );

			/// <summary>
			/// Run the event loop for the specified <paramref name="duration" />. 
			/// </summary>
			/// <param name="duration">The amount of time to run the event loop.</param>
			virtual void Run(TimeSpan duration);

			/// <summary>
			/// Run the event loop until a single event occurs, or the specified <paramref name="duration"/> has elapsed. 
			/// </summary>
			/// <param name="duration">The amount of time to run the event loop.</param>
			virtual void RunOnce(TimeSpan duration);
	
#if defined NETFX_40
			[System::ComponentModel::EditorBrowsableAttribute(System::ComponentModel::EditorBrowsableState::Never)]
			virtual System::Threading::Tasks::Task^ RunAsync(TimeSpan duration); // not implemented.

			[System::ComponentModel::EditorBrowsableAttribute(System::ComponentModel::EditorBrowsableState::Never)]
			virtual System::Threading::Tasks::Task^ RunOnceAsync(TimeSpan duration); // not implemented.
#endif
			/// <summary>
			/// Occurs when a paired Myo has been connected. 
			/// </summary>
			virtual event EventHandler<MyoEventArgs^>^ MyoConnected;

			/// <summary>
			/// Occurs when a paired Myo has been disconnected. 
			/// </summary>
			virtual event EventHandler<MyoEventArgs^>^ MyoDisconnected;
			
			/// <summary>
			/// Occurs when a Myo has been paired. 
			/// </summary>
			virtual event EventHandler<MyoEventArgs^>^ MyoPaired;

			/// <summary>
			/// Occurs when a Myo has been unpaired. 
			/// </summary>
			virtual event EventHandler<MyoEventArgs^>^ MyoUnpaired;
		};
	}
}

