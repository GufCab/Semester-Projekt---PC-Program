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
    public class CpConnectionManager
    {
       private Hashtable UnspecifiedTable = Hashtable.Synchronized(new Hashtable());
       internal UPnPService _S;

       public UPnPService GetUPnPService()
       {
            return(_S);
       }
       public static string SERVICE_NAME = "urn:schemas-upnp-org:service:ConnectionManager:";
       public double VERSION
       {
           get
           {
               return(double.Parse(_S.Version));
           }
       }

       public delegate void SubscribeHandler(CpConnectionManager sender, bool Success);
       public event SubscribeHandler OnSubscribe;
       public delegate void Delegate_OnResult_GetIPAddress(CpConnectionManager sender, System.String IPAddress, UPnPInvokeException e, object _Tag);
       private WeakEvent OnResult_GetIPAddress_Event = new WeakEvent();
       public event Delegate_OnResult_GetIPAddress OnResult_GetIPAddress
       {
			add{OnResult_GetIPAddress_Event.Register(value);}
			remove{OnResult_GetIPAddress_Event.UnRegister(value);}
       }

        public CpConnectionManager(UPnPService s)
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
        public System.String A_ARG_TYPE_IPAddress
        {
            get
            {
               return((System.String)_S.GetStateVariable("A_ARG_TYPE_IPAddress"));
            }
        }
        public bool HasStateVariable_A_ARG_TYPE_IPAddress
        {
            get
            {
               if (_S.GetStateVariableObject("A_ARG_TYPE_IPAddress")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public bool HasAction_GetIPAddress
        {
            get
            {
               if (_S.GetAction("GetIPAddress")==null)
               {
                   return(false);
               }
               else
               {
                   return(true);
               }
            }
        }
        public void Sync_GetIPAddress(out System.String IPAddress)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("IPAddress", "");
            _S.InvokeSync("GetIPAddress", args);
            IPAddress = (System.String) args[0].DataValue;
            return;
        }
        public void GetIPAddress()
        {
            GetIPAddress(null, null);
        }
        public void GetIPAddress(object _Tag, Delegate_OnResult_GetIPAddress _Callback)
        {
           UPnPArgument[] args = new UPnPArgument[1];
           args[0] = new UPnPArgument("IPAddress", "");
           _S.InvokeAsync("GetIPAddress", args, new object[2]{_Tag,_Callback}, new UPnPService.UPnPServiceInvokeHandler(Sink_GetIPAddress), new UPnPService.UPnPServiceInvokeErrorHandler(Error_Sink_GetIPAddress));
        }
        private void Sink_GetIPAddress(UPnPService sender, string MethodName, UPnPArgument[] Args, object RetVal, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetIPAddress)StateInfo[1])(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
            else
            {
                OnResult_GetIPAddress_Event.Fire(this, (System.String )Args[0].DataValue, null, StateInfo[0]);
            }
        }
        private void Error_Sink_GetIPAddress(UPnPService sender, string MethodName, UPnPArgument[] Args, UPnPInvokeException e, object _Tag)
        {
            object[] StateInfo = (object[])_Tag;
            if (StateInfo[1]!=null)
            {
                ((Delegate_OnResult_GetIPAddress)StateInfo[1])(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
            else
            {
                OnResult_GetIPAddress_Event.Fire(this, (System.String)UPnPService.CreateObjectInstance(typeof(System.String),null), e, StateInfo[0]);
            }
        }
    }
}