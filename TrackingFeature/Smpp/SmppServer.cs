using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Inetlab.SMPP;
using Inetlab.SMPP.Common;
using Inetlab.SMPP.PDU;

namespace TrackingFeature.Smpp
{
    public class SmppServer
    {
        private readonly SmppClient _smppClient = new SmppClient();

        public SmppClient Client
        {
            get { return _smppClient; }
        }

        public SmppServer()
        {
        }

        public void ConnectToSmppServerVianett()
        {
            Client.Connect("cpa.vianett.no", 2775);
            if (Client.Status == ConnectionStatus.Open)
            {
                Console.WriteLine("Connected to SMPP server");
                Client.Bind("atlanta_and@mail.ru", "hxncf");
                if (Client.Status == ConnectionStatus.Bound)
                {
                    Console.WriteLine("Bound with SMPP server");
                }
            }
        }

        public void ConnectToSmppServerSmsCarrier()
        {
            Client.Connect("213.158.112.40", 3342);
            if (Client.Status == ConnectionStatus.Open)
            {
                Console.WriteLine("Connected to SMPP server");
                Client.Bind("PRIwebBP1", "Mf1vdZrw");
                if (Client.Status == ConnectionStatus.Bound)
                {
                    Console.WriteLine("Bound with SMPP server");
                }
            }
        }

        public void SendSms()
        {
            var response = Client.Submit(SMS.ForSubmit()
                 .ServiceType("test")
                 .Text("Only .Net only C#!!!")
                 .From("QUIZUP")
                 .To("+37368453453")
                 .DeliveryReceipt()
                 .Coding(DataCodings.Default)
                 );
        }

        public void SendSmsOldAproach()
        {
            SubmitSm sm = new SubmitSm();
            sm.ServiceType = "test";
            sm.UserDataPdu.ShortMessage = Client.GetMessageBytes("Only .Net only C#!!!", DataCodings.Default);
            sm.SourceAddr = "Komisar";
            sm.SourceAddrTon = 0;
            sm.SourceAddrNpi = 1;
            sm.DestAddr = "+37367347545";
            sm.DestAddrTon = 0;
            sm.DestAddrNpi = 1;
            sm.DataCoding = DataCodings.Default;
            sm.RegisteredDelivery = 1;

            var response = Client.Submit(sm);
        }
    }
}
