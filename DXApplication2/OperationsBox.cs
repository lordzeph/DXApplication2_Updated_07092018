﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.Entity;
using DataHandler;
using DevExpress.XtraGrid.Views;
using DevExpress.XtraGrid.Views.Grid;

namespace DXApplication2
{
    public partial class OperationsBox : DevExpress.XtraEditors.XtraUserControl
    {
        bool init = true;
        bool IsEdit = false;
        bool IsNew = false;
        bool payerselected=false;
        ClientsServices payerclient;
      
        DataHandler.ContabilidadEntities dbContext = new DataHandler.ContabilidadEntities();
        OperationsOptionscs operationoptions = new OperationsOptionscs();
        PartnersOptions partnersoptions = new PartnersOptions();
        ClientsOptions clientoptions = new ClientsOptions();
        ContabilidadEntities usercont = new ContabilidadEntities();
        //List<UserCompanies> usercomplist = new List<UserCompanies>();
        //List<UserCompanies> usercomplistold = new List<UserCompanies>();
        List<ClientsServices> Selclientlist = new List<ClientsServices>();
        // List<ClientsSet> Oldclientlist = new List<ClientsSet>();
        List<PartnersSet> partnerlist = new List<PartnersSet>();
        List<PartnersServices> Selpartnerlist = new List<PartnersServices>();
        List<PartnersServices> newSelpartnerlist = new List<PartnersServices>();
        List<IncomesSet> serviceincomes = new List<IncomesSet>();
        // Permisions[] userpermissions;
        Users loggeduser;
        ServicesSet selectedOperation = new ServicesSet();
        ServicesSet openedoperation;
        Users serviceuser;

        AirTicketsSet  newplaneticket = new AirTicketsSet();
        AirTicketsSet selplaneticket = new AirTicketsSet();
        List<AirTicketsSet> planeticketlist = new List<AirTicketsSet>();
        List<AirTicketsSet> newplaneticketlist = new List<AirTicketsSet>();
        ToursSet newtour = new ToursSet();
        ToursSet seltour = new ToursSet();
        List<ToursSet> tourlist = new List<ToursSet>();
        List<ToursSet> newtourlist = new List<ToursSet>();
        TrainTicketsSet newtraintickect = new TrainTicketsSet();
        TrainTicketsSet seltraintickect = new TrainTicketsSet();
        List<TrainTicketsSet> trainticketlist = new List<TrainTicketsSet>();
        List<TrainTicketsSet> newtrainticketlist = new List<TrainTicketsSet>();
        TransferSet newtransfer = new TransferSet();
        TransferSet seltransfer = new TransferSet();
        List<TransferSet> transferlist = new List<TransferSet>();
        List<TransferSet> newtransferlist = new List<TransferSet>();
        HotelsSet newhotel = new HotelsSet();
        HotelsSet selhotel = new HotelsSet();
        List<HotelsSet> hotellist = new List<HotelsSet>();
        List<HotelsSet> newhotellist = new List<HotelsSet>();


        CashOptions cashoptions = new CashOptions();

        public void ForSeller()
        {
            ClientValueEdit.Enabled = false;
            ClientDateDCB.Enabled = false;
            ClientExchEdit.Enabled = false;
            ClientIsbankChB.Enabled = false;
            ClientCashBoxGV.Enabled = false;
            CcashDetailsEdit.Enabled = false;
            ClientAddCash.Enabled = false;

            PartvalueEdit.Enabled = false;
            PartDateDCB.Enabled = false;
            PartExchEdit.Enabled = false;
            PartnercashCB.Enabled = false;
            PartIsbankChB.Enabled = false;
            PartAddCash.Enabled = false;
            PartnerCashBoxGV.Enabled = false;
            PcashDetailsEdit.Enabled = false;
        }

        public void ForCashier()
        {
            ServiceCB.Enabled = false;
            SelPartnerlistChCB.Enabled = false;
            CreationDCB.Enabled = false;
            CountryCB.Enabled = false;
            PlannedProfCB.Enabled = false;
            BrutoEdit.Enabled = false;
            NetoEdit.Enabled = false;

            NameEdit.Enabled = false;
            ComissionEdit.Enabled = false;
            FromDateDCB.Enabled = false;
            ToDateDCB.Enabled = false;
            RealPEdit.Enabled = false;
            CreditEdit.Enabled = false;
            DebtEdit.Enabled = false;

            searchControl1.Enabled = false;
            ClienListGV.Enabled = false;
            PayerEdit.Enabled = false;
            TouristGV.Enabled = false;
            //tabPane1.Enabled = false;
            PlanesGV.Enabled = false;
            panelControl8.Enabled = false;
            panelControl7.Enabled = false;
            ExcursionsGV.Enabled = false;
            TrainsGV.Enabled = false;
            panelControl9.Enabled = false;
            TransfersGV.Enabled = false;
            panelControl10.Enabled = false;
            HotelsGV.Enabled = false;
            panelControl11.Enabled = false;

            tableLayoutPanel4.Enabled = true;
        }


        int usertypeID = 2;
        public OperationsBox(Users LoggedUser)
        {
            IsNew = true;
            loggeduser = LoggedUser;
            InitializeComponent();
            // InitTextboxes();

            ServiceCB.Properties.Items.AddRange(operationoptions.GetServicesTypes());
            CreationDCB.DateTime = DateTime.Now.Date;
            CountryCB.Properties.Items.AddRange(operationoptions.GetCountries());
            HcountryCB.Properties.Items.AddRange(CountryCB.Properties.Items);
            NameL.Text = loggeduser.Name;
            ClienListGV.DataSource = clientoptions.GetActiveClients();
            partnerlist.AddRange(partnersoptions.GetActivePartners());
            SelPartnerlistChCB.Properties.Items.AddRange(partnersoptions.GetActivePartnersItem());
            TouristGV.DataSource = Selclientlist;
            HotelsGV.DataSource = hotellist;
            PlanesGV.DataSource = planeticketlist;
            TrainsGV.DataSource = trainticketlist;
            ExcursionsGV.DataSource = tourlist;
            TransfersGV.DataSource = trainticketlist;

            // Code Added By Pushkar India
            if (loggeduser.UserTypeID.ToString().Equals("2"))
            {
                ForCashier();
            }
            else if (loggeduser.UserTypeID.ToString().Equals("3"))
            {
                ForSeller();
            }
            // Code Ends Here !!

            //TpartnersCB.Properties.Items.AddRange(SelPartnerlistChCB.Properties.Items.ToArray());
            //PpartnersCB.Properties.Items.AddRange(SelPartnerlistChCB.Properties.Items.ToArray());
            //TrpartnerCB.Properties.Items.AddRange(SelPartnerlistChCB.Properties.Items.ToArray());
            //EpartnerCB.Properties.Items.AddRange(SelPartnerlistChCB.Properties.Items.ToArray());
            //HpartnersCB.Properties.Items.AddRange(SelPartnerlistChCB.Properties.Items.ToArray());
            // This line of code is generated by Data Source Configuration Wizard
            // Instantiate a new DBContext
            //  DataHandler.ContabilidadEntities dbContext = new DataHandler.ContabilidadEntities();
            // Call the Load method to get the data for the given DbSet from the database.
            // dbContext.ClientsServices.Load();
            // This line of code is generated by Data Source Configuration Wizard
            // clientsServicesBindingSource.DataSource = dbContext.ClientsServices.Local.ToBindingList();
        }
        public OperationsBox(ServicesSet openedservice,Users LoggedUser)
        {
            //openedoperation = openedservice;
            payerselected = true;
            openedoperation = new ServicesSet()
            {
                Comission = openedservice.Comission,
                CompanyID = openedservice.CompanyID,
                CountryId = openedservice.CountryId,
                CreationDate = openedservice.CreationDate,
                FromDate = openedservice.FromDate,
                NetPrice = openedservice.NetPrice,
                PrecioBruto = openedservice.PrecioBruto,
                Servicename = openedservice.Servicename,
                Specs = openedservice.Specs,
                ToDate = openedservice.ToDate,
                UserId = openedservice.UserId,
                SeviceTypeID = openedservice.SeviceTypeID,
                ServiceId = openedservice.ServiceId
                

            };
            // selectedOperation = openedservice;
            selectedOperation = new ServicesSet()
            {

                Comission = openedservice.Comission,
                CompanyID = openedservice.CompanyID,
                CountryId = openedservice.CountryId,
                CreationDate = openedservice.CreationDate,
                FromDate = openedservice.FromDate,
                NetPrice = openedservice.NetPrice,
                PrecioBruto = openedservice.PrecioBruto,
                Servicename = openedservice.Servicename,
                Specs = openedservice.Specs,
                ToDate = openedservice.ToDate,
                UserId = openedservice.UserId,
                SeviceTypeID = openedservice.SeviceTypeID,
                ServiceId = openedservice.ServiceId
                
            };
            IsEdit = true;
            loggeduser = LoggedUser;
            InitializeComponent();
            // InitTextboxes();
            ServiceCB.Properties.Items.AddRange(operationoptions.GetServicesTypes());


            // CreationDCB.DateTime = openedoperation.CreationDate;
            OperartionIDL.Text = openedservice.ServiceId.ToString();
            CountryCB.Properties.Items.AddRange(operationoptions.GetCountries());
            HcountryCB.Properties.Items.AddRange(CountryCB.Properties.Items);
            NameL.Text = loggeduser.Name;
            ClienListGV.DataSource = clientoptions.GetActiveClients();
            partnerlist.AddRange(partnersoptions.GetActivePartners());
            SelPartnerlistChCB.Properties.Items.AddRange(partnersoptions.GetActivePartnersItem());
          Selpartnerlist.AddRange(operationoptions.GetServicePartners(openedservice.ServiceId));
            for (int i = 0; i < SelPartnerlistChCB.Properties.Items.Count; i++)
            {
                if (Selpartnerlist.Exists(e=>e.PartnersSet.PartnerName == SelPartnerlistChCB.Properties.Items[i].Value.ToString()))
                    SelPartnerlistChCB.Properties.Items[i].CheckState = CheckState.Checked;
                TpartnersCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
                PpartnersCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
                TrpartnerCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
                EpartnerCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
                HpartnersCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
                PartnercashCB.Properties.Items.Add(SelPartnerlistChCB.Properties.Items[i].Value);
            }
            //for (int i = 0; i < Selpartnerlist.Count; i++)
            //{
            //    PartCB.Properties.Items.Add(Selpartnerlist[i].PartnersSet.PartnerName);
            //}
           
          Selclientlist.AddRange(operationoptions.GetTouristClients(openedservice.ServiceId));
            payerclient = operationoptions.GetPayerclient(openedservice.ServiceId);
           PayerEdit.Text = payerclient.ClientsSet.IDName;
            //Oldclientlist.AddRange(Selclientlist.ToArray());
           // Oldclientlist.Add(payerclient);
           serviceuser = openedservice.Users;
            TouristGV.DataSource = Selclientlist;
            ComissionEdit.Text = openedservice.Comission;
            BrutoEdit.Text = openedservice.PrecioBruto.ToString();
            NetoEdit.Text = openedservice.NetPrice.ToString();
            FromDateDCB.DateTime = openedservice.FromDate;
            ToDateDCB.DateTime = openedservice.ToDate;
            CountryCB.Text = openedservice.CountriesSet.CountryName;
            ServiceCB.Text = openedservice.ServiceTypes.ServiceType;
            CreationDCB.DateTime = openedservice.CreationDate;
            NameEdit.Text = openedservice.Servicename;
         



            tourlist.AddRange(operationoptions.Getexcursiondata(openedoperation.ServiceId));
            ExcursionsGV.DataSource =tourlist ;
            transferlist.AddRange(operationoptions.Gettransferdata(openedoperation.ServiceId));
            TransfersGV.DataSource = transferlist ;
            trainticketlist.AddRange(operationoptions.Gettraindata(openedoperation.ServiceId));
            TrainsGV.DataSource = trainticketlist;
            planeticketlist.AddRange(operationoptions.GetPlanedata(openedoperation.ServiceId));
            PlanesGV.DataSource = planeticketlist;
            hotellist.AddRange(operationoptions.GetHotelsdata(openedoperation.ServiceId));
            HotelsGV.DataSource = hotellist;

            ClientCashBoxGV.DataSource = cashoptions.GetServiceIncomes(openedoperation.ServiceId);
            PartnerCashBoxGV.DataSource = cashoptions.GetServiceExpenses(openedoperation.ServiceId);

            //Permissions
            switch (loggeduser.UserTypeID)
            {
                case 2:
                    tableLayoutPanel1.Enabled = false;
                    tableLayoutPanel2.Enabled = false;
                    panelControl3.Enabled = false;
                    tabPane1.Enabled = false;

                    break;

                case 3:
                    
                    panelControl4.Enabled = false;
                    panelControl5.Enabled = false;


                    break;

                default:
                    break;
            }
        }

        private void simpleButton14_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            BoxContainer outbox = new BoxContainer(2, loggeduser);
            outbox.Show();
        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            BoxContainer outbox = new BoxContainer(3, loggeduser);
            outbox.FormClosed += Outbox_FormClosed;
            outbox.Show();
        }

        private void Outbox_FormClosed(object sender, FormClosedEventArgs e)
        {
            ClienListGV.DataSource = clientoptions.GetActiveClients();
            gridView1.RefreshData();
        }

        private void ClienListGV_DoubleClick(object sender, EventArgs e)
        {
            string selclient = gridView1.GetFocusedRowCellValue("IDName").ToString();
            var client = ((ClientsSet[]) gridView1.DataSource)[gridView1.ViewRowHandleToDataSourceIndex(gridView1.FocusedRowHandle)];
              
            if (payerselected)
            {
                if (!Selclientlist.Exists(f => f.ClientsSet == client))
                {
                 
                   
                    Selclientlist.Add(new ClientsServices() { ClientsSet = client, IsPayer = false, Clientid = client.ClientId });
                    gridView6.RefreshData();

                }
                    
            }
            else
            {

                PayerEdit.Text = selclient;
                payerselected = true;
                payerclient = new ClientsServices() { ClientsSet = client, IsPayer = true, Clientid = client.ClientId };

           
            }
              

        }

        private void SelPartnerlistChCB_EditValueChanged(object sender, EventArgs e)
        {
       
        }

        private void SelPartnerlistChCB_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            
        }


        private void InserPartnerServices (int servicedid)
        {




            // List<PartnersSet> chekedpartList = new List<PartnersSet>();

            //ServicesSet old = selectedOperation;
            // var checkedlist = SelPartnerlistChCB.Properties.Items.GetCheckedValues();
            if (!IsEdit)
            {
                for (int i = 0; i < Selpartnerlist.Count; i++)
            {
                operationoptions.addPartnerService( servicedid, Selpartnerlist[i].PartnerId );
              //  chekedpartList.Add(operationoptions.getparnerSer(checkedlist[0].ToString(),selectedOperation));
          // usercont.ServicesSet.Local[0].PartnersSet.Add(usercont.PartnersSet.Local[0]);
      //    selectedOperation.PartnersServices.Add(new PartnersServices() { PartnerId = operationoptions.getparnerSer(checkedlist[0].ToString(), selectedOperation).PartnerId});
            }
            }
            else
            {
                for (int i = 0; i < newSelpartnerlist.Count; i++)
                {
                    operationoptions.addPartnerService(servicedid, newSelpartnerlist[i].PartnerId);
                    //  chekedpartList.Add(operationoptions.getparnerSer(checkedlist[0].ToString(),selectedOperation));
                    // usercont.ServicesSet.Local[0].PartnersSet.Add(usercont.PartnersSet.Local[0]);
                    //    selectedOperation.PartnersServices.Add(new PartnersServices() { PartnerId = operationoptions.getparnerSer(checkedlist[0].ToString(), selectedOperation).PartnerId});
                }
            }
                

           // usercont.SaveChanges();
           

          //   selectedOperation.PartnersSet = chekedpartList;
          
         //   usercont.ServicesSet.Local.Add(selectedOperation);
            
          // usercont.Entry(selectedOperation.PartnersSet).CurrentValues.SetValues(chekedpartList);
           // operationoptions.UpdatePartnerServices(chekedpartList.ToArray(), chekedpartList.ToArray());


           // selectedOperation.SeviceTypeID = operationoptions.GetServicesID((Text);
        }
        private void Validate_Leave(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "CreationDCB":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            selectedOperation.CreationDate =DateTime.Parse(dfg.Text) ;

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "FromDateDCB":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            selectedOperation.FromDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "ToDateDCB":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            selectedOperation.ToDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "BrutoEdit":
                        if (Validatedata(dfg.Text))
                        {
                            
                            dfg.BackColor = Color.White;
                          selectedOperation.PrecioBruto = int.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "NetoEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                          
                         selectedOperation.NetPrice = int.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                      

                        break;

                    case "NameEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            selectedOperation.Servicename = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ComissionEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            selectedOperation.Comission= dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ServiceCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            
                            selectedOperation.SeviceTypeID = operationoptions.GetServicesTypeId(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                   

                }


        }

        private bool Validatedata(string data)
        {

            if (String.IsNullOrWhiteSpace(data))
                return false;
           
            return true;


        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            // Selclientlist.Where("IDName =" + TouristLB.SelectedValue.ToString());
            Selclientlist.RemoveAt(gridView6.GetFocusedDataSourceRowIndex());
            gridView6.RefreshData();
          
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            payerselected = false;
            payerclient = null;

            PayerEdit.Text = string.Empty;

        }

        private void insertClientServices(int servicedid)
        {
            List<ClientsServices> clientserList = new List<ClientsServices>();
           // ClientsServices payer = new ClientsServices() {  IsPayer = true,Clientid = payerclient.Clientid };
            operationoptions.addclientService(payerclient.Clientid, servicedid,true);
         //  selectedOperation.ClientsServices.Add(payer);
            for (int i = 0; i < Selclientlist.Count; i++)
            {
                // ClientsServices tourist = new ClientsServices() {  IsPayer = false, Clientid = Selclientlist[i].ClientId };
                //  selectedOperation.ClientsServices.Add(tourist);
                operationoptions.addclientService(Selclientlist[i].Clientid, servicedid,false);
                // clientserList.Add(tourist);
            }
           // operationoptions.UpdateClientsServices(clientserList.ToArray(), clientserList.ToArray());


        }

        private void CommitChanges()
        {
            int serviceid;
            if (IsEdit)
                {
                
                operationoptions.UpdateService(openedoperation, selectedOperation);
                if (payerselected)
                    Selclientlist.Add(payerclient);
                    operationoptions.UpdateClientsServices(openedoperation.ServiceId, Selclientlist.ToArray());
                //operationoptions.UpdatepartnersServices(openedoperation.ServiceId, Selpartnerlist.ToArray());
             serviceid = openedoperation.ServiceId;
                }
            else
            {
            selectedOperation.CompanyID = 1;
            selectedOperation.CreationDate = DateTime.Now;
            selectedOperation.UserId = loggeduser.UserID;
            selectedOperation.Specs = "xxx";
            selectedOperation.CountryId = operationoptions.GetCountrybyName(CountryCB.Text);
            
            operationoptions.addOperation(selectedOperation);
            //  usercont.ServicesSet.Add(selectedOperation);
            //  usercont.SaveChanges();
          serviceid = operationoptions.GetServiceID(selectedOperation.Servicename);
           
            insertClientServices(serviceid);
        
            }
               InserPartnerServices(serviceid);
      

            //insert planedata
            for (int i = 0; i < newplaneticketlist.Count; i++)
            {
                newplaneticketlist[i].PartnerServiceId = operationoptions.GetServicePartnerID(newplaneticketlist[i].PartnerServiceId, serviceid);
                operationoptions.addPlaneticket(newplaneticketlist[i]); 
            }

            //insert trains
            for (int i = 0; i < newtrainticketlist.Count; i++)
            {
                newtrainticketlist[i].PartnerServiceId = operationoptions.GetServicePartnerID(newtrainticketlist[i].PartnerServiceId, serviceid);
                operationoptions.addTrain(newtrainticketlist[i]);
            }

            //insert transfers
            for (int i = 0; i < newtransferlist.Count; i++)
            {
                newtransferlist[i].PartnerServiceId = operationoptions.GetServicePartnerID(newtransferlist[i].PartnerServiceId, serviceid);
                operationoptions.addTransfer(newtransferlist[i]);
            }

            //insert excursions
            for (int i = 0; i < newtourlist.Count; i++)
            {
                newtourlist[i].PartnersServiceId = operationoptions.GetServicePartnerID(newtourlist[i].PartnersServiceId, serviceid);
                operationoptions.addTour(newtourlist[i]);
            }

            //insert hotels
            for (int i = 0; i < newhotellist.Count; i++)
            {
                newhotellist[i].ServicesPartnerId = operationoptions.GetServicePartnerID(newhotellist[i].ServicesPartnerId, serviceid);
                operationoptions.addHotel(newhotellist[i]);
            }
     
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CommitChanges();




        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            IsEdit = true;
        }

        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {

        }

        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

        }

        private void ClientAddCash_Click(object sender, EventArgs e)
        {
            cashoptions.AddIncome(new IncomesSet() { Value = decimal.Parse(ClientValueEdit.EditValue.ToString()), IsBankPaid = ClientIsbankChB.Checked, CoinExchange = decimal.Parse(ClientExchEdit.EditValue.ToString()), FactDate = ClientDateDCB.DateTime, ServiceId = openedoperation.ServiceId,Details = CcashDetailsEdit.Text });
            ClientCashBoxGV.DataSource = cashoptions.GetServiceIncomes(selectedOperation.ServiceId);
            cashclientView.RefreshData();
        }

        private void PartAddCash_Click(object sender, EventArgs e)
        {
            cashoptions.AddExpense(new ExpensesSet() { PartnerServicesID = operationoptions.GetServicePartnerID(PartnercashCB.Text,selectedOperation.ServiceId), BankPaid = PartIsbankChB.Checked, Value = decimal.Parse(PartvalueEdit.EditValue.ToString()), CoinExange = decimal.Parse(PartExchEdit.EditValue.ToString()), FactDate = PartDateDCB.DateTime, Details = PcashDetailsEdit.Text });
            PartnerCashBoxGV.DataSource = cashoptions.GetServiceExpenses(selectedOperation.ServiceId);
            cashpartView.RefreshData();
        }

        private void TravelView_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {

            TravelView.GetDataSourceRowIndex(e.RowHandle);
        }

        private void PlanesGV_Click(object sender, EventArgs e)
        {
          
        }

       

        private void PlanesGV_DataSourceChanged(object sender, EventArgs e)
        {

        }

        private void TravelView_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            repositoryItemComboBox3.Items.AddRange(partnersoptions.GetActivePartners());
            GridView view = sender as GridView;
            //view.Columns["PartnersServices.PartnersSet.PartnerName"].da = repositoryItemComboBox3;
            //view.SetRowCellValue(e.RowHandle, view.Columns["TourName"],partnersoptions.GetActivePartners());
           // view.SetRowCellValue(e.RowHandle, view.Columns["Name"], "CustomName");
           // view.SetRowCellValue(e.RowHandle, view.Columns["Notes"], "New Note");

        }

        private void HotelsGV_Click(object sender, EventArgs e)
        {

        }

        private void labelControl111_Click(object sender, EventArgs e)
        {

        }

       

        private void Validate_Leave_Planes(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "PnameEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newplaneticket.TicketName = dfg.Text;
                   

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "PcreationdateEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newplaneticket.CreationDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "PnetpriceEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newplaneticket.NetPrice =  decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "PbrutpriceEdit":
                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newplaneticket.Brutprice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "PofferpriceEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.OfferPrice = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PofferspanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.OfferSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PoutdateEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.OutDate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PbrutpricespanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.BrutPriceSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PcodeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Code = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ProuteEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Route = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PdepartsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.DepartTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ParrivesEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.ArriveTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PclassEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.ConfortClass = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PnumberEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Number = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PtypeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Type = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PpartnersCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newplaneticket.PartnerServiceId = operationoptions.getparner(dfg.Text).PartnerId;
                            
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PdetailsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Details = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PDepartstoEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.DepartPlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "PdarrivessatEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.ArrivePlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;





                }


        }

        private void Validate_Leave_Excurcions(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "EnameEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtour.TourName = dfg.Text;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "ECDEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtour.CreationDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "EnetpriceEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtour.NetPrice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "EBrutEdit":
                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtour.Brutprice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "EofferEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.OfferPrice = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EofferspanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.OfferSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EoutdateEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.OutDate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EbrutpricespanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.BrutPriceSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EcodeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Code = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ErouteEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Route = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;
                      
                    case "EdepartsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.DepartTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EdepartstoEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.DepartPlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EarrivesatEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.ArrivePlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EarrivesEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.ArriveTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EclassEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.ConfortClass = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EnumberEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Number = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EtypeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Type = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EpartnerCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtour.PartnersServiceId =  operationoptions.getparner(dfg.Text).PartnerId;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EdetailsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newplaneticket.Details = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "Etransport1Edit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Transport1 = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;


                    case "Etransport2Edit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.Transport2 = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "EtouristsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtour.TouristCount = int.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;





                }


        }

       

        private void Validate_Leave_Trains(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "TnameEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtraintickect.Name = dfg.Text;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "TcreationdateEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtraintickect.CreationDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "TnetpriceEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtraintickect.NetPrice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "TbrutpriceEdit":
                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtraintickect.Brutprice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "TofferpriceEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.OfferPrice = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TofferspanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.OfferSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "ToutdateEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.OutDate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TbrutpricespanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.BrutPriceSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TcodeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.Code = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrouteEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.Route = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TdepartsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.DepartTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TarrivesEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.ArriveTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TclassEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.ConfortClass = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TnumberEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.Number = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TtypeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.Type = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TpartnersCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtraintickect.PartnerServiceId = operationoptions.getparner(dfg.Text).PartnerId;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TdetailsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.Details = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TdepartstoEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.DepartPlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TarrivesatEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtraintickect.ArrivePlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;





                }


        }

        private void Validate_Leave_Transfers(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "TrnameEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtransfer.TransferName = dfg.Text;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "TrcreationdateEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtransfer.CreationDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "TrnetpriceEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newtransfer.NetPrice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "TrbrutpriceEdit":
                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtransfer.Brutprice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "TrofferpriceEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.OfferPrice = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrofferspanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.OfferSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TroutdateEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.OutDate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrbrutpricesspanEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.BrutPriceSpan = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrcodeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.Code = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrrouteEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.Route = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrdepartsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.DepartTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrarrivesEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.ArriveTime = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrclassEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.ConfortClass = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrnumberEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.Number = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrtypeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.Type = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrpartnerCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newtransfer.PartnerServiceId = operationoptions.getparner(dfg.Text).PartnerId;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrdetailsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.Details = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrdepartstoEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.DepartPlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "TrarrivesatEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newtransfer.ArrivePlace = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;





                }


        }

        private void Validate_Leave_Hotels(object sender, EventArgs e)
        {
            TextEdit dfg = sender as TextEdit;
            if (IsEdit)
                dfg.BackColor = Color.White;
            if (String.IsNullOrWhiteSpace(dfg.Text) & IsEdit)
                dfg.BackColor = Color.PeachPuff;
            if (dfg.EditValue != null & !string.IsNullOrEmpty(dfg.Text))
                switch (dfg.Name)
                {
                    case "HnameEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newhotel.Hotelname = dfg.Text;



                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "HcreationdateEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newhotel.CreationDate = DateTime.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;

                    case "HnetpriceEdit":

                        if (Validatedata(dfg.Text))
                        {
                            dfg.BackColor = Color.White;
                            newhotel.Netprice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }
                        break;
                    case "HbrutpriceEdit":
                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newhotel.BrutPrice = decimal.Parse(dfg.Text);

                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }

                        break;
                    case "HofferpriceEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.OfferPrice = decimal.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HofferendEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.OfferEndDate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HofferstartEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.OfferStartdate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HchekinEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.CheckinDate= DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HcheckoutEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.CheckOutdate = DateTime.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HfoodEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                           newhotel.Food = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HroomEdit":

                       if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.Room = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HcityEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.City = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HnightsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.Nights = int.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;


                    case "HcatEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.HotelCat = int.Parse(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HcodeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.HotelCode = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                   

                    

                   

                    case "HtypeEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.Type = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HpartnersCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                            newhotel.ServicesPartnerId = operationoptions.getparner(dfg.Text).PartnerId;


                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HdetailsEdit":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;

                            newhotel.HotelDetails = dfg.Text;
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                    case "HcountryCB":

                        if (Validatedata(dfg.Text))
                        {

                            dfg.BackColor = Color.White;
                           
                           newhotel.CountryId =  operationoptions.GetCountrybyName(dfg.Text);
                        }
                        else
                        {
                            dfg.BackColor = Color.PeachPuff;
                        }


                        break;

                   





                }


        }
 private void simpleButton19_Click(object sender, EventArgs e)
        {
            if (EditTourB.Text == "Edit")
            {
                if (newtourlist.Exists(w => w.TourName == seltour.TourName))
                {
                    var cache = newtourlist.Find(y => y.TourName == seltour.TourName);
                    newtourlist[newtourlist.IndexOf(cache)] = newtour;
                    tourlist[tourlist.IndexOf(cache)] = newtour;


                }
                else
                {
                    operationoptions.UpdateTour(seltour, newtour);
                    tourlist.Clear();
                    tourlist.AddRange(operationoptions.Getexcursiondata(selectedOperation.ServiceId));
                }
            }
            else
            {

                newtour.Isguided = EguidedChb.Checked;
                tourlist.Add(newtour);
                
                newtourlist.Add(newtour);
            }
            TravelView.RefreshData();
            //operationoptions.addTour(newtour);
        }

        //Add Services types
        private void simpleButton17_Click(object sender, EventArgs e)
        {
            if ( EdittransferB.Text == "Edit")
            {
                if ( newtransferlist.Exists(w=>w.TransferName == seltransfer.TransferName))
                {
                    var cache = newtransferlist.Find(y => y.TransferName == seltransfer.TransferName);
                    newtransferlist[newtransferlist.IndexOf(cache)] = newtransfer;
                    transferlist[transferlist.IndexOf(cache)] = newtransfer;


                }
                else
                {
                    operationoptions.Updatetransfer(seltransfer, newtransfer);
                    transferlist.Clear();
                    transferlist.AddRange(operationoptions.Gettransferdata(selectedOperation.ServiceId));
                    transferlist.AddRange(newtransferlist.ToArray());
                }
            }
            else
            {
                transferlist.Add(newtransfer);
          
            newtransferlist.Add(newtransfer);
            initboxestransf();
            }
  TrasnferView.RefreshData();

            
           
           // operationoptions.addTransfer(newtransfer);
        }

 private void simpleButton13_Click(object sender, EventArgs e)
        {
            if (EditPlanesB.Text == "Edit")
            {
                if (newplaneticketlist.Exists(w=> w.TicketName ==selplaneticket.TicketName))
                    {
                    var cache = newplaneticketlist.Find(y => y.TicketName == selplaneticket.TicketName);
                    newplaneticketlist[newplaneticketlist.IndexOf(cache)] = newplaneticket;

                    }
                else
                {
                    operationoptions.UpdatePlaneticket(selplaneticket, newplaneticket);
                    planeticketlist.Clear();
                    planeticketlist.AddRange(operationoptions.GetPlanedata(selectedOperation.ServiceId));
                }
            }
            else
            {
            planeticketlist.Add(newplaneticket);
           
            newplaneticketlist.Add(newplaneticket);
            initboxesplanes();
            }
               PlaneView.RefreshData();
           // operationoptions.addPlaneticket(newplaneticket);
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            if (EdittrainB.Text == "Edit")
            {
                if (newtrainticketlist.Exists(w => w.Name == seltraintickect.Name))
                {
                    var cache = newtrainticketlist.Find(y => y.Name == seltraintickect.Name);
                    newtrainticketlist[newtrainticketlist.IndexOf(cache)] = newtraintickect;
                    trainticketlist[trainticketlist.IndexOf(cache)] = newtraintickect;


                }
                else
                {
                    operationoptions.UpdateTrainticket(seltraintickect, newtraintickect);
                    transferlist.Clear();
                    transferlist.AddRange(operationoptions.Gettransferdata(selectedOperation.ServiceId));
                }
            }
            else
            {
                trainticketlist.Add(newtraintickect);
                
                newtrainticketlist.Add(newtraintickect);
                initboxestrain();
            }
            TrainsView.RefreshData();
           // operationoptions.addTrain(newtraintickect);
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {
            if (EdithotelB.Text == "Edit")
            {
                if (newhotellist.Exists(w => w.Hotelname == selhotel.Hotelname))
                {
                    var cache = newhotellist.Find(y => y.Hotelname == selhotel.Hotelname);
                    newhotellist[newhotellist.IndexOf(cache)] = newhotel;
                    hotellist[hotellist.IndexOf(cache)] = newhotel;


                }
                else
                {
                    operationoptions.UpdateHotels(selhotel, newhotel);
                    hotellist.Clear();
                    hotellist.AddRange(operationoptions.GetHotelsdata(selectedOperation.ServiceId));
                }
            }
            else
            {
                hotellist.Add(newhotel);
                
                newhotellist.Add(newhotel);
                initboxeshotels();
            }
            HotelsView.RefreshData();
           
        }

        private void PofferspanEdit_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void PofferspanEdit_Leave(object sender, EventArgs e)
        {

        }

        private void PoutdateEdit_Leave(object sender, EventArgs e)
        {

        }

        private void SelPartnerlistChCB_Leave(object sender, EventArgs e)
        {
          
            for (int i = 0; i < SelPartnerlistChCB.Properties.Items.Count; i++)
            {
                string partname = SelPartnerlistChCB.Properties.Items[i].Value.ToString();
                if (SelPartnerlistChCB.Properties.Items[i].CheckState == CheckState.Checked)
                {
                   
                    if (!Selpartnerlist.Exists(r => r.PartnersSet.PartnerName == partname ))
                    {
                        var ptoinsert = partnerlist.Single(t => t.PartnerName == partname);
                        PartnersServices newpartser = new PartnersServices() { PartnerId = ptoinsert.PartnerId , ServiceId = 0,PartnersSet = ptoinsert };
                        if (!IsEdit)
                        {
                            Selpartnerlist.Add(newpartser);
                        }
                        else
                        {
                           newSelpartnerlist.Add(newpartser);
                        }
                            

                           
                        
                    }
                }
                else if (SelPartnerlistChCB.Properties.Items[i].CheckState == CheckState.Unchecked)
                {
                    if (Selpartnerlist.Exists(r => r.PartnersSet.PartnerName == partname))
                    {
                        SelPartnerlistChCB.Properties.Items[i].CheckState = CheckState.Checked;
                       // Selpartnerlist.RemoveAll(f=> f.PartnersSet.PartnerName == partname);
                    }

                    if (newSelpartnerlist.Exists(r => r.PartnersSet.PartnerName == partname))
                    {
                        //SelPartnerlistChCB.Properties.Items[i].CheckState = CheckState.Checked;
                         newSelpartnerlist.RemoveAll(f=> f.PartnersSet.PartnerName == partname);
                       // HpartnersCB.Properties.Items.Clear();

                    }
                }

               // operationoptions.addPartnerService(servicedid, operationoptions.getparner(checkedlist[0].ToString()).PartnerId);
               //  chekedpartList.Add(operationoptions.getparnerSer(checkedlist[0].ToString(),selectedOperation));
               // usercont.ServicesSet.Local[0].PartnersSet.Add(usercont.PartnersSet.Local[0]);
               //    selectedOperation.PartnersServices.Add(new PartnersServices() { PartnerId = operationoptions.getparnerSer(checkedlist[0].ToString(), selectedOperation).PartnerId});
            }
            TpartnersCB.Properties.Items.Clear();
            PpartnersCB.Properties.Items.Clear();
            TrpartnerCB.Properties.Items.Clear();
            EpartnerCB.Properties.Items.Clear();
            HpartnersCB.Properties.Items.Clear();
            PartnercashCB.Properties.Items.Clear();
            var checkedlist = SelPartnerlistChCB.Properties.Items.GetCheckedValues();
            TpartnersCB.Properties.Items.AddRange(checkedlist);
            PpartnersCB.Properties.Items.AddRange(checkedlist);
            TrpartnerCB.Properties.Items.AddRange(checkedlist);
            EpartnerCB.Properties.Items.AddRange(checkedlist);
            HpartnersCB.Properties.Items.AddRange(checkedlist);
            PartnercashCB.Properties.Items.AddRange(checkedlist);


        }

        //Load row data

        private void loadHotelsEditform()
        {
            selhotel = ((List<HotelsSet>)HotelsView.DataSource)[HotelsView.ViewRowHandleToDataSourceIndex(HotelsView.FocusedRowHandle)];

            newhotel = selhotel;

            HchekinEdit.DateTime = selhotel.CheckinDate.Value;
            HcheckoutEdit.DateTime = selhotel.CheckOutdate.Value;
            HcityEdit.Text = selhotel.City;

            HfoodEdit.Text = selhotel.Food;
            HcatEdit.Text = selhotel.HotelCat.ToString();
            HcodeEdit.Text = selhotel.HotelCode;
            HdetailsEdit.Text = selhotel.HotelDetails;
            HnameEdit.Text = selhotel.Hotelname;
            HcreationdateEdit.DateTime = selhotel.CreationDate;
            HnightsEdit.Text = selhotel.Nights.ToString();
            HofferendEdit.DateTime = selhotel.OfferEndDate.Value;
            HofferstartEdit.DateTime = selhotel.OfferStartdate.Value;
            HroomEdit.Text = selhotel.Room;
            HbrutpriceEdit.Text = selhotel.BrutPrice.ToString();
            HnetpriceEdit.Text = selhotel.Netprice.ToString();
            HofferpriceEdit.Text = selhotel.OfferPrice.ToString();

            HtypeEdit.Text = selhotel.Type;
        }
        private void initHotelsEditform()
        {



            HchekinEdit.ResetText();
            HcheckoutEdit.ResetText();
            HcityEdit.Text = string.Empty;

            HfoodEdit.Text = string.Empty;
            HcatEdit.Text = string.Empty;
            HcodeEdit.Text = string.Empty;
            HdetailsEdit.Text = string.Empty;
            HnameEdit.Text = string.Empty;
            HcreationdateEdit.ResetText();
            HnightsEdit.Text = string.Empty;
            HofferendEdit.ResetText();
            HofferstartEdit.ResetText();
            HroomEdit.Text = string.Empty;
            HbrutpriceEdit.Text = string.Empty;
            HnetpriceEdit.Text = string.Empty;
            HofferpriceEdit.Text = string.Empty;

            HtypeEdit.Text = string.Empty;
        }

        private void loadTrasnferEditform()
        {
            seltransfer = ((List<TransferSet>)TrasnferView.DataSource)[TrasnferView.ViewRowHandleToDataSourceIndex(TrasnferView.FocusedRowHandle)];
            newtransfer = seltransfer;
            TrarrivesatEdit.Text = seltransfer.ArrivePlace;
                  TrarrivesEdit.DateTime = seltransfer.ArriveTime.Value;
                TrbrutpriceEdit.Text = seltransfer.Brutprice.ToString();
                  TrbrutpricesspanEdit.DateTime = seltransfer.BrutPriceSpan.Value;
                 TrcodeEdit.Text = seltransfer.Code;
                  TrclassEdit.Text = seltransfer.ConfortClass;
                 TrcreationdateEdit.DateTime = seltransfer.CreationDate;
                 TrdepartstoEdit.Text = seltransfer.DepartPlace;
                TrdepartsEdit.DateTime = seltransfer.DepartTime.Value;
                 TrdetailsEdit.Text = seltransfer.Details;
                 TrnetpriceEdit.Text = seltransfer.NetPrice.ToString();
                 TrnumberEdit.Text = seltransfer.Number;
                  TrofferpriceEdit.Text = seltransfer.OfferPrice.ToString();
                  TrofferspanEdit.DateTime = seltransfer.OfferSpan.Value;
                  TroutdateEdit.DateTime = seltransfer.OutDate.Value;
                 
                  TrrouteEdit.Text = seltransfer.Route;
                  TrnameEdit.Text = seltransfer.TransferName;
                  TrtypeEdit.Text = seltransfer.Type;


        }
        private void initTrasnferEditform()
        {

            TrarrivesatEdit.Text = string.Empty;
            TrarrivesEdit.ResetText();
            TrbrutpriceEdit.Text = string.Empty;
            TrbrutpricesspanEdit.ResetText();
            TrcodeEdit.Text = string.Empty;
            TrclassEdit.Text = string.Empty;
            TrcreationdateEdit.ResetText();
            TrdepartstoEdit.Text = string.Empty; ;
            TrdepartsEdit.ResetText();
            TrdetailsEdit.Text = string.Empty;
            TrnetpriceEdit.Text = string.Empty;
            TrnumberEdit.Text = string.Empty;
            TrofferpriceEdit.Text = string.Empty;
            TrofferspanEdit.ResetText();
            TroutdateEdit.ResetText();

            TrrouteEdit.Text = string.Empty;
            TrnameEdit.Text = string.Empty;
            TrtypeEdit.Text = string.Empty;


        }

        private void loadTrainsEditform()
        {
            seltraintickect = ((List<TrainTicketsSet>)TrainsView.DataSource)[TrainsView.ViewRowHandleToDataSourceIndex(TrainsView.FocusedRowHandle)];
            newtraintickect = seltraintickect;
            TarrivesatEdit.Text = seltraintickect.ArrivePlace;
            TarrivesEdit.DateTime = seltraintickect.ArriveTime.Value;
            TbrutpriceEdit.Text = seltraintickect.Brutprice.ToString();
            TbrutpricespanEdit.DateTime = seltraintickect.BrutPriceSpan.Value;
            TcodeEdit.Text = seltraintickect.Code;
            TclassEdit.Text = seltraintickect.ConfortClass;
            TcreationdateEdit.DateTime = seltraintickect.CreationDate;
            TdepartstoEdit.Text = seltraintickect.DepartPlace;
            TdepartsEdit.DateTime = seltraintickect.DepartTime.Value;
            TdetailsEdit.Text = seltraintickect.Details;
            TnetpriceEdit.Text = seltraintickect.NetPrice.ToString();
            TnumberEdit.Text = seltraintickect.Number;
            TofferpriceEdit.Text = seltraintickect.OfferPrice.ToString();
            TofferspanEdit.DateTime = seltraintickect.OfferSpan.Value;
            ToutdateEdit.DateTime = seltraintickect.OutDate.Value;

            TrouteEdit.Text = seltraintickect.Route;
            TnameEdit.Text = seltraintickect.Name;
            TtypeEdit.Text = seltraintickect.Type;


        }

    

        private void initTrainsEditform()
        {

            TarrivesatEdit.Text = string.Empty;
            TarrivesEdit.ResetText();
            TbrutpriceEdit.Text = string.Empty;
            TbrutpricespanEdit.ResetText();
            TcodeEdit.Text = string.Empty;
            TclassEdit.Text = string.Empty;
            TcreationdateEdit.ResetText();
            TdepartstoEdit.Text = string.Empty;
            TdepartsEdit.ResetText();
            TdetailsEdit.Text = string.Empty;
            TnetpriceEdit.Text = string.Empty;
            TnumberEdit.Text = string.Empty;
            TofferpriceEdit.Text = string.Empty;
            TofferspanEdit.ResetText();
            ToutdateEdit.ResetText();

            TrouteEdit.Text = string.Empty;
            TnameEdit.Text = string.Empty;
            TtypeEdit.Text = string.Empty;


        }

        private void loadTourEditform()
        {
            seltour = ((List<ToursSet>)TravelView.DataSource)[TravelView.ViewRowHandleToDataSourceIndex(TravelView.FocusedRowHandle)];
            newtour = seltour;
            EarrivesatEdit.Text = seltour.ArrivePlace;
            EarrivesEdit.DateTime = seltour.ArriveTime.Value;
            EBrutEdit.Text = seltour.Brutprice.ToString();
            EbrutpricespanEdit.DateTime = seltour.BrutPriceSpan.Value;
            EcodeEdit.Text = seltour.Code;
            EclassEdit.Text = seltour.ConfortClass;
            ECDEdit.DateTime = seltour.CreationDate;
            EdepartstoEdit.Text = seltour.DepartPlace;
            EdepartsEdit.DateTime = seltour.DepartTime.Value;
            Edetailsedit.Text = seltour.Details;
            EnetpriceEdit.Text = seltour.NetPrice.ToString();
            EnumberEdit.Text = seltour.Number;
            EofferEdit.Text = seltour.OfferPrice.ToString();
            EofferspanEdit.DateTime = seltour.OfferSpan.Value;
            EoutdateEdit.DateTime = seltour.OutDate.Value;

            ErouteEdit.Text = seltour.Route;
            EnameEdit.Text = seltour.TourName;
            EtypeEdit.Text = seltour.Type;
            EtouristsEdit.Text = seltour.TouristCount.ToString();


        }

        private void initTourEditform()
        {

            EarrivesatEdit.Text = string.Empty;
            EarrivesEdit.ResetText();
            EBrutEdit.Text = string.Empty;
            EbrutpricespanEdit.ResetText();
            EcodeEdit.Text = string.Empty;
            EclassEdit.Text = string.Empty;
            ECDEdit.DateTime = DateTime.Now;
            EdepartstoEdit.Text = string.Empty;
            EdepartsEdit.ResetText();
            Edetailsedit.Text = string.Empty;
            EnetpriceEdit.Text = string.Empty;
            EnumberEdit.Text = string.Empty;
            EofferEdit.Text = string.Empty;
            EofferspanEdit.ResetText();
            EoutdateEdit.ResetText();

            ErouteEdit.Text = string.Empty;
            EnameEdit.Text = string.Empty;
            EtypeEdit.Text = string.Empty;


        }

        private void loadPlanesEditform()
        {
            selplaneticket = ((List<AirTicketsSet>)PlaneView.DataSource)[PlaneView.ViewRowHandleToDataSourceIndex(PlaneView.FocusedRowHandle)];
            newplaneticket = selplaneticket;
            PdarrivessatEdit.Text = selplaneticket.ArrivePlace;
            ParrivesEdit.DateTime = selplaneticket.ArriveTime;
            PbrutpriceEdit.Text = selplaneticket.Brutprice.ToString();
            PbrutpricespanEdit.DateTime = selplaneticket.BrutPriceSpan;
            PcodeEdit.Text = selplaneticket.Code.ToString();
            PclassEdit.Text = selplaneticket.ConfortClass;
            PcreationdateEdit.DateTime = selplaneticket.CreationDate;
            PDepartstoEdit.Text = selplaneticket.DepartPlace;
            PdepartsEdit.DateTime = selplaneticket.DepartTime;
            PdetailsEdit.Text = selplaneticket.Details;
            PnetpriceEdit.Text = selplaneticket.NetPrice.ToString();
            PnumberEdit.Text = selplaneticket.Number.ToString();
            PofferpriceEdit.Text = selplaneticket.OfferPrice.ToString();
            PofferspanEdit.DateTime = selplaneticket.OfferSpan;
            PoutdateEdit.DateTime = selplaneticket.OutDate;

            ProuteEdit.Text = selplaneticket.Route;
            PnameEdit.Text = selplaneticket.TicketName;
            PtypeEdit.Text = selplaneticket.Type;
        //    PpartnersCB.Text = selplaneticket.PartnersServices.PartnersSet.PartnerName;


        }
       


        //Init service types  editforms
        private void initboxestransf()
        {
            int partserid = newtransfer.PartnerServiceId;
            int transfid = newtransfer.TransferId;
            newtransfer = new TransferSet()
            {
                ArrivePlace = TrarrivesatEdit.Text,
                ArriveTime = TrarrivesEdit.DateTime,
                Brutprice = decimal.Parse(TrbrutpriceEdit.Text),
                BrutPriceSpan = TrbrutpricesspanEdit.DateTime,
                Code = TrcodeEdit.Text,
                ConfortClass = TrclassEdit.Text,
                CreationDate = TrcreationdateEdit.DateTime,
                DepartPlace = TrdepartstoEdit.Text,
                DepartTime = TrdepartsEdit.DateTime,
                Details = TrdetailsEdit.Text,
                NetPrice = decimal.Parse(TrnetpriceEdit.Text),
                Number = TrnumberEdit.Text,
                OfferPrice = decimal.Parse(TrofferpriceEdit.Text),
                OfferSpan = TrofferspanEdit.DateTime,
                OutDate = TroutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = TrrouteEdit.Text,
                TransferName = TrnameEdit.Text,
                Type = TrtypeEdit.Text,
                TransferId = transfid
                

            };

            seltransfer = new TransferSet()
            {
                ArrivePlace = TrarrivesatEdit.Text,
                ArriveTime = TrarrivesEdit.DateTime,
                Brutprice = decimal.Parse(TrbrutpriceEdit.Text),
                BrutPriceSpan = TrbrutpricesspanEdit.DateTime,
                Code = TrcodeEdit.Text,
                ConfortClass = TrclassEdit.Text,
                CreationDate = TrcreationdateEdit.DateTime,
                DepartPlace = TrdepartstoEdit.Text,
                DepartTime = TrdepartsEdit.DateTime,
                Details = TrdetailsEdit.Text,
                NetPrice = decimal.Parse(TrnetpriceEdit.Text),
                Number = TrnumberEdit.Text,
                OfferPrice = decimal.Parse(TrofferpriceEdit.Text),
                OfferSpan = TrofferspanEdit.DateTime,
                OutDate = TroutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = TrrouteEdit.Text,
                TransferName = TrnameEdit.Text,
                Type = TrtypeEdit.Text,
                TransferId = transfid

            };
        }

        private void initboxestrain()
        {
            int partserid = newtraintickect.PartnerServiceId;
            int trainid = newtraintickect.TrainTicketId;

            newtraintickect = new TrainTicketsSet()
            {
                ArrivePlace = TarrivesatEdit.Text,
                ArriveTime = TarrivesEdit.DateTime,
                Brutprice = decimal.Parse(TbrutpriceEdit.Text),
                BrutPriceSpan = TbrutpricespanEdit.DateTime,
                Code = TcodeEdit.Text,
                ConfortClass = TclassEdit.Text,
                CreationDate = TcreationdateEdit.DateTime,
                DepartPlace = TdepartstoEdit.Text,
                DepartTime = TdepartsEdit.DateTime,
                Details = TdetailsEdit.Text,
                NetPrice = decimal.Parse(TnetpriceEdit.Text),
                Number = TnumberEdit.Text,
                OfferPrice = decimal.Parse(TofferpriceEdit.Text),
                OfferSpan = TofferspanEdit.DateTime,
                OutDate = ToutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = TrouteEdit.Text,
                Name = TnameEdit.Text,
                Type = TtypeEdit.Text,
                TouristCount = 0,
                TrainTicketId = trainid

            };

            seltraintickect = new TrainTicketsSet()
            {
                ArrivePlace = TarrivesatEdit.Text,
                ArriveTime = TarrivesEdit.DateTime,
                Brutprice = decimal.Parse(TbrutpriceEdit.Text),
                BrutPriceSpan = TbrutpricespanEdit.DateTime,
                Code = TcodeEdit.Text,
                ConfortClass = TclassEdit.Text,
                CreationDate = TcreationdateEdit.DateTime,
                DepartPlace = TdepartstoEdit.Text,
                DepartTime = TdepartsEdit.DateTime,
                Details = TdetailsEdit.Text,
                NetPrice = decimal.Parse(TnetpriceEdit.Text),
                Number = TnumberEdit.Text,
                OfferPrice = decimal.Parse(TofferpriceEdit.Text),
                OfferSpan = TofferspanEdit.DateTime,
                OutDate = ToutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = TrouteEdit.Text,
                Name = TnameEdit.Text,
                Type = TtypeEdit.Text,
                TouristCount = 0,
                TrainTicketId = trainid

            };
        }

        private void initboxesplanes()
        {
            int partserid = newplaneticket.PartnerServiceId;
            int planeid = newplaneticket.AirTicketId;
            newplaneticket = new AirTicketsSet()
            {
                ArrivePlace = PdarrivessatEdit.Text,
                ArriveTime = ParrivesEdit.DateTime,
                Brutprice = decimal.Parse(PbrutpriceEdit.Text),
                BrutPriceSpan = PbrutpricespanEdit.DateTime,
                Code = decimal.Parse(PcodeEdit.Text),
                ConfortClass = PclassEdit.Text,
                CreationDate = PcreationdateEdit.DateTime,
                DepartPlace = PDepartstoEdit.Text,
                DepartTime = PdepartsEdit.DateTime,
                Details = PdetailsEdit.Text,
                NetPrice = decimal.Parse(PnetpriceEdit.Text),
                Number = decimal.Parse(PnumberEdit.Text),
                OfferPrice = decimal.Parse(PofferpriceEdit.Text),
                OfferSpan = TrofferspanEdit.DateTime,
                OutDate = PoutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = ProuteEdit.Text,
                TicketName = PnameEdit.Text,
                Type = PtypeEdit.Text,
                AirTicketId = planeid



            };

            selplaneticket = new AirTicketsSet()
            {
                ArrivePlace = PdarrivessatEdit.Text,
                ArriveTime = ParrivesEdit.DateTime,
                Brutprice = decimal.Parse(PbrutpriceEdit.Text),
                BrutPriceSpan = PbrutpricespanEdit.DateTime,
                Code = decimal.Parse(PcodeEdit.Text),
                ConfortClass = PclassEdit.Text,
                CreationDate = PcreationdateEdit.DateTime,
                DepartPlace = PDepartstoEdit.Text,
                DepartTime = PdepartsEdit.DateTime,
                Details = PdetailsEdit.Text,
                NetPrice = decimal.Parse(PnetpriceEdit.Text),
                Number = decimal.Parse(PnumberEdit.Text),
                OfferPrice = decimal.Parse(PofferpriceEdit.Text),
                OfferSpan = TrofferspanEdit.DateTime,
                OutDate = PoutdateEdit.DateTime,
                PartnerServiceId = partserid,
                Route = ProuteEdit.Text,
                TicketName = PnameEdit.Text,
                Type = PtypeEdit.Text,
                AirTicketId = planeid



            };
        }

        private void initboxestour()
        {
            int partserid = newtour.PartnersServiceId;
            int tourid = newtour.TourId;
            newtour = new ToursSet()
            {
                ArrivePlace = EarrivesatEdit.Text,
                ArriveTime = EarrivesEdit.DateTime,
                Brutprice = decimal.Parse(EBrutEdit.Text),
                BrutPriceSpan = EbrutpricespanEdit.DateTime,
                Code = EcodeEdit.Text,
                ConfortClass = EclassEdit.Text,
                CreationDate = ECDEdit.DateTime,
                DepartPlace = EdepartstoEdit.Text,
                DepartTime = EdepartsEdit.DateTime,
                Details = Edetailsedit.Text,
                NetPrice = decimal.Parse(EnetpriceEdit.Text),
                Number = EnumberEdit.Text,
                OfferPrice = decimal.Parse(EofferEdit.Text),
                OfferSpan = EofferspanEdit.DateTime,
                OutDate = EoutdateEdit.DateTime,
                PartnersServiceId = partserid,
                Route = ErouteEdit.Text,
                TourName = EnameEdit.Text,
                Type = EtypeEdit.Text,
                TouristCount = int.Parse(EtouristsEdit.Text),
                TourId = tourid

            };

            seltour = new ToursSet()
            {
                ArrivePlace = EarrivesatEdit.Text,
                ArriveTime = EarrivesEdit.DateTime,
                Brutprice = decimal.Parse(EBrutEdit.Text),
                BrutPriceSpan = EbrutpricespanEdit.DateTime,
                Code = EcodeEdit.Text,
                ConfortClass = EclassEdit.Text,
                CreationDate = ECDEdit.DateTime,
                DepartPlace = EdepartstoEdit.Text,
                DepartTime = EdepartsEdit.DateTime,
                Details = Edetailsedit.Text,
                NetPrice = decimal.Parse(EnetpriceEdit.Text),
                Number = EnumberEdit.Text,
                OfferPrice = decimal.Parse(EofferEdit.Text),
                OfferSpan = EofferspanEdit.DateTime,
                OutDate = EoutdateEdit.DateTime,
                PartnersServiceId = partserid,
                Route = ErouteEdit.Text,
                TourName = EnameEdit.Text,
                Type = EtypeEdit.Text,
                TouristCount = int.Parse(EtouristsEdit.Text),
                TourId = tourid

            };
        }

        private void initboxeshotels()
        {
            int partserid = newhotel.ServicesPartnerId;
            int countryid = newhotel.CountryId;
            int hotelid = newhotel.HotelId;
            newhotel = new HotelsSet()
            {
                CheckinDate = HchekinEdit.DateTime,
                CheckOutdate = HcheckoutEdit.DateTime,
                City = HcityEdit.Text,
                CountryId = countryid,
                Food = HfoodEdit.Text,
                HotelCat = int.Parse(HcatEdit.Text),
                HotelCode = HcodeEdit.Text,
                HotelDetails = HdetailsEdit.Text,
                Hotelname = HnameEdit.Text,
                CreationDate = HcreationdateEdit.DateTime,
                Nights = int.Parse(HnightsEdit.Text),
                OfferEndDate = HofferendEdit.DateTime,
                OfferStartdate = HofferstartEdit.DateTime,
                Room = HroomEdit.Text,
                BrutPrice = decimal.Parse(HbrutpriceEdit.Text),
                Netprice = decimal.Parse(HnetpriceEdit.Text),
                OfferPrice = decimal.Parse(HofferpriceEdit.Text),
                ServicesPartnerId = partserid,
                Type = HtypeEdit.Text,
                HotelId = hotelid


            };

            selhotel = new HotelsSet()
            {
                CheckinDate = HchekinEdit.DateTime,
                CheckOutdate = HcheckoutEdit.DateTime,
                City = HcityEdit.Text,
                CountryId = countryid,
                Food = HfoodEdit.Text,
                HotelCat = int.Parse(HcatEdit.Text),
                HotelCode = HcodeEdit.Text,
                HotelDetails = HdetailsEdit.Text,
                Hotelname = HnameEdit.Text,
                CreationDate = HcreationdateEdit.DateTime,
                Nights = int.Parse(HnightsEdit.Text),
                OfferEndDate = HofferendEdit.DateTime,
                OfferStartdate = HofferstartEdit.DateTime,
                Room = HroomEdit.Text,
                BrutPrice = decimal.Parse(HbrutpriceEdit.Text),
                Netprice = decimal.Parse(HnetpriceEdit.Text),
                OfferPrice = decimal.Parse(HofferpriceEdit.Text),
                ServicesPartnerId = partserid,
                Type = HtypeEdit.Text,
                HotelId = hotelid


            };
        }

        private void initPlanesEditform()
        {
            PdarrivessatEdit.Text = string.Empty;
            ParrivesEdit.ResetText();
            PbrutpriceEdit.Text = string.Empty;
            PbrutpricespanEdit.ResetText();
            PcodeEdit.Text = string.Empty;
            PclassEdit.Text = string.Empty;
            PcreationdateEdit.ResetText();
            PDepartstoEdit.Text = string.Empty;
            PdepartsEdit.ResetText();
            PdetailsEdit.Text = string.Empty;
            PnetpriceEdit.Text = string.Empty;
            PnumberEdit.Text = string.Empty;
            PofferpriceEdit.Text = string.Empty;
            PofferspanEdit.ResetText();
            PoutdateEdit.ResetText();
            ProuteEdit.Text = string.Empty;
            PnameEdit.Text = string.Empty;
            PtypeEdit.Text = string.Empty;
            PpartnersCB.Text = string.Empty;

        }

        private void PlaneView_Click(object sender, EventArgs e)
        {
           
                initPlanesEditform();
            newplaneticket = new AirTicketsSet();
                EditPlanesB.Text = "Add";
            
        }

        private void PlaneView_RowClick(object sender, RowClickEventArgs e)
        {

            loadPlanesEditform();
            initboxesplanes();
            
            EditPlanesB.Text = "Edit";

        }

        private void TravelView_RowClick(object sender, RowClickEventArgs e)
        {
            loadTourEditform();
            initboxestour();
            EditTourB.Text = "Edit";

        }

        private void TravelView_Click(object sender, EventArgs e)
        {
            initTourEditform();
            newtour = new ToursSet();
            EditTourB.Text = "Add";

        }

        private void TrainsView_RowClick(object sender, RowClickEventArgs e)
        {
            loadTrainsEditform();
            initboxestrain();
            EdittrainB.Text = "Edit";
        }

        private void TrainsView_Click(object sender, EventArgs e)
        {
            initTrainsEditform();
            newtraintickect = new TrainTicketsSet();
            EdittrainB.Text = "Add";

        }

        private void TrasnferView_RowClick(object sender, RowClickEventArgs e)
        {
            loadTrasnferEditform();
            initboxestransf();
            EdittransferB.Text = "Edit";

        }

        private void TrasnferView_Click(object sender, EventArgs e)
        {
            initTrasnferEditform();
            newtransfer = new TransferSet();
            EdittransferB.Text = "Add";

        }

        private void HotelsView_RowClick(object sender, RowClickEventArgs e)
        {
            loadHotelsEditform();
            initboxeshotels();
            EdithotelB.Text = "Edit";






        }

        private void HotelsView_Click(object sender, EventArgs e)
        {
            initHotelsEditform();
            newhotel = new HotelsSet();
            EdithotelB.Text = "Add";
        }

        private void OperationsBox_Load(object sender, EventArgs e)
        {

        }
    }
    
}
