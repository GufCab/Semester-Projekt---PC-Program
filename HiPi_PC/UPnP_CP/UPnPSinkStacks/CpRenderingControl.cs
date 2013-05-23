using System;
using System.Collections;
using System.Threading;
using OpenSource.Utilities;
using OpenSource.UPnP;

namespace SinkStack
{
    /// <summary>
    /// Transparent ClientSide UPnP Service
    /// </summary>
    public class CpRenderingControl
    {
       private Hashtable UnspecifiedTable = Hashtable.Synchronized(new Hashtable());
       internal UPnPService _S;

       public UPnPService GetUPnPService()
       {
            return(_S);
       }
       public static string SERVICE_NAME = "urn:schemas-upnp-org:service:RenderingControl:";
       public double VERSION
       {
           get
           {
               return(double.Parse(_S.Version));
           }
       }

       public delegate void SubscribeHandler(CpRenderingControl sender, bool Success);
       public event SubscribeHandler OnSubscribe;
       public delegate void Delegate_OnResult_GetPosition(CpRenderingControl sender, System.UInt32 InstanceID, System.UInt16 CurrentPosition, System.UInt16 Duration, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetPosition_Event = new WeakEvent();
       public event Delegate_OnResult_GetPosition OnResult_GetPosition
       {
			add{OnResult_GetPosition_Event.Register(value);}
			remove{OnResult_GetPosition_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_GetVolume(CpRenderingControl sender, System.UInt32 InstanceID, System.String Channel, System.UInt16 CurrentVolume, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetVolume_Event = new WeakEvent();
       public event Delegate_OnResult_GetVolume OnResult_GetVolume
       {
			add{OnResult_GetVolume_Event.Register(value);}
			remove{OnResult_GetVolume_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetMute(CpRenderingControl sender, System.UInt32 InstanceID, System.String Channel, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetMute_Event = new WeakEvent();
       public event Delegate_OnResult_SetMute OnResult_SetMute
       {
			add{OnResult_SetMute_Event.Register(value);}
			remove{OnResult_SetMute_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetPosition(CpRenderingControl sender, System.UInt32 InstanceID, System.UInt16 DesiredPosition, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetPosition_Event = new WeakEvent();
       public event Delegate_OnResult_SetPosition OnResult_SetPosition
       {
			add{OnResult_SetPosition_Event.Register(value);}
			remove{OnResult_SetPosition_Event.UnRegister(value);}
       }
       public delegate void Delegate_OnResult_SetVolume(CpRenderingControl sender, System.UInt32 InstanceID, System.String Channel, System.UInt16 DesiredVolume, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_SetVolume_Event = new WeakEvent();
       public event Delegate_OnResult_SetVolume OnResult_SetVolume
       {
			add{OnResult_SetVolume_Event.Register(value);}
			remove{OnResult_SetVolume_Event.UnRegister(value);}
       }

        public CpRenderingControl(UPnPService s)
        {
            _S = s;
            _S.OnSubscribe += new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
        }
        public void Dispose()
        {
            _S.OnSubscribe -= new UPnPService.UPnPEventSubscribeHandler(_subscribe_sink);
            OnSubscribe = null;
        }
        public void _subscribe(int Timeout)
        {
            _S.Subscribe(Timeout, null);
        }
        protected void _subscribe_sink(UPnPService sender, bool OK)
        {
            if (OnSubscribe!=null)
            {
                OnSubscribe(this, OK);
            }
        }
        public void SetUnspecifiedValue(string EnumType, string val)
        {
            string hash = Thread.CurrentThread.GetHashCode().ToString() + ":" + EnumType;
            UnspecifiedTable[hash] = val;
        }
        public string GetUnspecifiedValue(string EnumType)
        {
            string hash = Thread.CurrentThread.GetHashCode().ToString() + ":" + EnumType;
            if (UnspecifiedTable.ContainsKey(hash)==false)
            {
               return("");
            }
            string RetVal = (string)UnspecifiedTable[hash];
            return(RetVal);
        }
        public System.UInt16 Position
        {
            get
            {
               return((System.UInt16)_S.GetStateVariable("Position"));
            }
        }
        public System.String A_ARG_TYPE_Channel
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_Channel"));
            }
        }
        public System.UInt16 TrackDur
        {
            get
            {
               return((System.UInt16)_S.GetStateVariable("TrackDur"));
            }
        }
        public System.UInt16 Volume
        {
            get
            {
               return((System.UInt16)_S.GetStateVariable("Volume"));
            }
        }
        public System.UInt32 A_ARG_TYPE_InstanceID
        {
            get
            {
               return((System.UInt32)_S.GetStateVariable("A_ARG_TYPE_InstanceID"));
            }
        }
        public bool HasStateVariable_Position
        {
            get
            {
               if (_S.GetStateVariableObject("Position")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_Channel
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_Channel")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_TrackDur
        {
            get
            {
               if (_S.GetStateVariableObject("TrackDur")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_Volume
        {
            get
            {
               if (_S.GetStateVariableObject("Volume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_InstanceID
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_InstanceID")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetPosition
        {
            get
            {
               if (_S.GetAction("GetPosition")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetVolume
        {
            get
            {
               if (_S.GetAction("GetVolume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetMute
        {
            get
            {
               if (_S.GetAction("SetMute")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetPosition
        {
            get
            {
               if (_S.GetAction("SetPosition")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_SetVolume
        {
            get
            {
               if (_S.GetAction("SetVolume")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public void Sync_GetPosition(System.UInt32 InstanceID, out System.UInt16 CurrentPosition, out System.UInt16 Duration)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentPosition", "");
           args[2] = new UPnPArgument("Duration", "");
            _S.InvokeSync("GetPosition", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            CurrentPosition = (System.UInt16) args[1].DataValue;
            Duration = (System.UInt16) args[2].DataValue;
            return;
        }
        public void GetPosition(System.UInt32 InstanceID)
        {
            GetPosition(InstanceID, null, null);
        }
        public void GetPosition(System.UInt32 InstanceID, object _Tag, Delegate_OnResult_GetPosition _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("CurrentPosition", "");
           args[2] = new UPnPArgument("Duration", "");
           _S.InvokeAsync("GetPosition", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetPosition), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetPosition));
        }
        private void Sink_GetPosition(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetPosition)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetPosition_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetPosition(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetPosition)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetPosition_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
        }
        public void Sync_GetVolume(System.UInt32 InstanceID, System.String Channel, out System.UInt16 CurrentVolume)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
           args[2] = new UPnPArgument("CurrentVolume", "");
            _S.InvokeSync("GetVolume", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (System.String) args[1].DataValue;
            CurrentVolume = (System.UInt16) args[2].DataValue;
            return;
        }
        public void GetVolume(System.UInt32 InstanceID, System.String Channel)
        {
            GetVolume(InstanceID, Channel, null, null);
        }
        public void GetVolume(System.UInt32 InstanceID, System.String Channel, object _Tag, Delegate_OnResult_GetVolume _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
           args[2] = new UPnPArgument("CurrentVolume", "");
           _S.InvokeAsync("GetVolume", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetVolume), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetVolume));
        }
        private void Sink_GetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16)UPnPService.CreateObjectInstance(typeof(System.UInt16),null), e, StateInfo[0]);
            }
        }
        public void Sync_SetMute(System.UInt32 InstanceID, System.String Channel)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
            _S.InvokeSync("SetMute", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (System.String) args[1].DataValue;
            return;
        }
        public void SetMute(System.UInt32 InstanceID, System.String Channel)
        {
            SetMute(InstanceID, Channel, null, null);
        }
        public void SetMute(System.UInt32 InstanceID, System.String Channel, object _Tag, Delegate_OnResult_SetMute _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
           _S.InvokeAsync("SetMute", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetMute), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetMute));
        }
        private void Sink_SetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetMute(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetMute)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetMute_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetPosition(System.UInt32 InstanceID, System.UInt16 DesiredPosition)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("DesiredPosition", DesiredPosition);
            _S.InvokeSync("SetPosition", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            DesiredPosition = (System.UInt16) args[1].DataValue;
            return;
        }
        public void SetPosition(System.UInt32 InstanceID, System.UInt16 DesiredPosition)
        {
            SetPosition(InstanceID, DesiredPosition, null, null);
        }
        public void SetPosition(System.UInt32 InstanceID, System.UInt16 DesiredPosition, object _Tag, Delegate_OnResult_SetPosition _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[2];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("DesiredPosition", DesiredPosition);
           _S.InvokeAsync("SetPosition", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetPosition), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetPosition));
        }
        private void Sink_SetPosition(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetPosition)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetPosition_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetPosition(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetPosition)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetPosition_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.UInt16 )Args[1].DataValue, e, StateInfo[0]);
            }
        }
        public void Sync_SetVolume(System.UInt32 InstanceID, System.String Channel, System.UInt16 DesiredVolume)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
           args[2] = new UPnPArgument("DesiredVolume", DesiredVolume);
            _S.InvokeSync("SetVolume", args);
            InstanceID = (System.UInt32) args[0].DataValue;
            Channel = (System.String) args[1].DataValue;
            DesiredVolume = (System.UInt16) args[2].DataValue;
            return;
        }
        public void SetVolume(System.UInt32 InstanceID, System.String Channel, System.UInt16 DesiredVolume)
        {
            SetVolume(InstanceID, Channel, DesiredVolume, null, null);
        }
        public void SetVolume(System.UInt32 InstanceID, System.String Channel, System.UInt16 DesiredVolume, object _Tag, Delegate_OnResult_SetVolume _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[3];
           args[0] = new UPnPArgument("InstanceID", InstanceID);
           args[1] = new UPnPArgument("Channel", Channel);
           args[2] = new UPnPArgument("DesiredVolume", DesiredVolume);
           _S.InvokeAsync("SetVolume", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_SetVolume), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_SetVolume));
        }
        private void Sink_SetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_SetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_SetVolume(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_SetVolume)StateInfo[1])(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, e, StateInfo[0]);
            }
            else
            {
                OnResult_SetVolume_Event.Fire(this, (System.UInt32 )Args[0].DataValue, (System.String )Args[1].DataValue, (System.UInt16 )Args[2].DataValue, e, StateInfo[0]);
            }
        }
    }
}