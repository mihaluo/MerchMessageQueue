using System;
using System.Globalization;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Publisher;
using Xunit;

namespace MarchMessageQueue.Tests.Publisher
{
    public class RetryPublishTest
    {
        [Fact]
        public void RetryMessagePublish()
        {
            IRetryPublisher publisher = new RabbitMqRetryPublisher();
            publisher.Publish(new RetryMessage());
        }

        [Fact]
        public void SayHelloMessagePublish()
        {
            #region orderxml

            string orderXml = @"<?xml version=""1.0""?>
<DATASET>
<TABLE>
 <ThirdKeyId>14550700361360412_7</ThirdKeyId>
 <KeyId>0BA43790-DABD-40A9-A207-6591723452C9</KeyId>
 <CombinationOrderKeyId></CombinationOrderKeyId>
 <OrderCode>P151602100001013</OrderCode>
 <AgentKeyId>AA4A0918-9058-C208-2D7A-08D2B740F08D</AgentKeyId>
 <AgentName>四川酷秀旅游开发有限公司</AgentName>
 <SupplierKeyID>D97C512D-525D-CD2F-C3E3-08D1F15DDF6E</SupplierKeyID>
 <SupplierName>千佛崖</SupplierName>
 <OrderProductType>1</OrderProductType>
 <OrderType>2</OrderType>
 <ETicketDistributeType>1</ETicketDistributeType>
 <SendETicketMode>1</SendETicketMode>
 <Status>32</Status>
 <MobileNumberToGetETicket>13468698509</MobileNumberToGetETicket>
 <TotalAmount>245</TotalAmount>
 <TotalConsumedFeeAmount>245</TotalConsumedFeeAmount>
 <TotalReturnFeeAmount>0</TotalReturnFeeAmount>
 <TotalOverTimeFeeAmount>0</TotalOverTimeFeeAmount>
 <TotalProductVersionNum>1</TotalProductVersionNum>
 <TotalTicketQuantity>7</TotalTicketQuantity>
 <OrderInstruction></OrderInstruction>
 <TripDate>2016-02-10</TripDate>
 <SubmitDate>2016-02-10T10:07:27</SubmitDate>
 <BillDate>2016-02-10T10:07:44</BillDate>
 <FeeRate>1</FeeRate>
 <WithDrawDate>2016-04-09</WithDrawDate>
 <SendETicketDate>2016-02-10T10:07:44</SendETicketDate>
 <CreateDate>2016-02-10T10:07:27</CreateDate>
 <CreateBy>4BFA635F-CDED-4590-91AD-03E629662EFD</CreateBy>
 <LastUpdateDate>2016-02-10T10:19:59</LastUpdateDate>
 <LastUpdateBy>3657E7CB-3D70-477D-95F0-5B66BC6F212C</LastUpdateBy>
 <ProductVersionName>千佛崖风景区门票 成人 票</ProductVersionName>
 <ProductVersionPrice>35</ProductVersionPrice>
 <TimesToCheck></TimesToCheck>
 <TimesChecked>1</TimesChecked>
 <TimesToServe>7</TimesToServe>
 <TimesServed>7</TimesServed>
 <WithDrawAmount>0</WithDrawAmount>
 <ExpiredAmount>0</ExpiredAmount>
 <TicketCustomerType>1</TicketCustomerType>
 <CertificateType>1</CertificateType>
 <CertificateNumber></CertificateNumber>
 <NameOfTravelAgency></NameOfTravelAgency>
 <PayAccountType>64</PayAccountType>
 <PayBillCode>P151602100001013</PayBillCode>
 <ProductVersionKeyId>E8FDF186-C44E-44A7-B8D8-65D6C61BBDAE</ProductVersionKeyId>
 <FlagFromAPI>1</FlagFromAPI>
 <Source>3</Source>
 <ThirdKeyId>14550700361360412_7</ThirdKeyId>
 <APIAccountKeyId>1640BF9C-8611-4898-962D-FCBFFC3B8D9A</APIAccountKeyId>
 <BrokerRutingAmount>0</BrokerRutingAmount>
 <Type>1</Type>
 <ProductSource>0</ProductSource>
 <CarFlagKeyId>BC40277B-AC73-451D-9E6E-04E37EBA450F</CarFlagKeyId>
 <APITotalAmount>245</APITotalAmount>
 <IsSend>4</IsSend>
 <ProviderKeyID></ProviderKeyID>
 <IsNotBuildETicket>0</IsNotBuildETicket>
 <TopValidCode></TopValidCode>
 <CustomerRealName>哈燕</CustomerRealName>
 <TicketCode>1541-9724-1562</TicketCode>
 <SupplierEarnings>175</SupplierEarnings>
 <PartnerEarnings>70</PartnerEarnings>
 <FreezeServe>0</FreezeServe>
 <PartnerKeyId>C48EBA9B-DFFF-C37D-A97C-08D1ECB3F3C6</PartnerKeyId>
 <OSVersion>3.4.1.1</OSVersion>
 <AgentFeeRate>0</AgentFeeRate>
 <OrderSystemType>2</OrderSystemType>
 <PaySuccessTimes>1</PaySuccessTimes>
 <AwaitVerifyDay>0</AwaitVerifyDay>
</TABLE>
</DATASET>";

            #endregion

            for (int i = 0; i < 10000; i++)
            {
                IPublisher publisher = new RabbitMqPublish();
                publisher.Publish(new SayHelloMessage
                {
                    Message = DateTime.Now.ToString(CultureInfo.InvariantCulture) + orderXml
                });
            }
          
        }
    }
}