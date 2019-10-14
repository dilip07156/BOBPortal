using System;
using CardHolder.Utility;

namespace BobCardBackOffice
{
   
    #region MonthlyDetail Model
    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public class MonthlyDetail
    {
        /// <summary>
        /// Gets or sets the months.
        /// </summary>
        /// <value>The months.</value>
        /// <remarks></remarks>
        public string Months { get; set; }
        /// <summary>
        /// Gets or sets the payment.
        /// </summary>
        /// <value>The payment.</value>
        /// <remarks></remarks>
        public string payment { get; set; }
        /// <summary>
        /// Gets or sets the interest paid.
        /// </summary>
        /// <value>The interest paid.</value>
        /// <remarks></remarks>
        public string interestPaid { get; set; }
        /// <summary>
        /// Gets or sets the principal paid.
        /// </summary>
        /// <value>The principal paid.</value>
        /// <remarks></remarks>
        public string principalPaid { get; set; }
        /// <summary>
        /// Gets or sets the balance.
        /// </summary>
        /// <value>The balance.</value>
        /// <remarks></remarks>
        public string balance { get; set; }
        /// <summary>
        /// Gets or sets the monthly interest.
        /// </summary>
        /// <value>The monthly interest.</value>
        /// <remarks></remarks>
        public string monthlyInterest { get; set; }
        /// <summary>
        /// Gets or sets the cumulative principle.
        /// </summary>
        /// <value>The cumulative principle.</value>
        /// <remarks></remarks>
        public string cumulativePrinciple { get; set; }
        /// <summary>
        /// Gets or sets the cumulative interest.
        /// </summary>
        /// <value>The cumulative interest.</value>
        /// <remarks></remarks>
        public string cumulativeInterest { get; set; }
        /// <summary>
        /// Gets or sets the cumulative payment.
        /// </summary>
        /// <value>The cumulative payment.</value>
        /// <remarks></remarks>
        public string cumulativePayment { get; set; }
        
       // public MonthlyDetail(){}
        //public string getMonths()
        //{
        //    return months;
        //}

        //public string getPayment()
        //{
        //    return payment;
        //}

        //public string getInterestPaid()
        //{
        //    return interestPaid;
        //}

        //public string getPrincipalPaid()
        //{
        //    return principalPaid;
        //}

        //public string getBalance()
        //{
        //    return balance;
        //}

        //public void setMonths(string Value)
        //{
        //    months = Value;
        //}

        //public void setPayment(string Value)
        //{
        //    payment = Value;
        //}

        //public void setInterestPaid(string Value)
        //{
        //    interestPaid = Value;
        //}

        //public void setPrincipalPaid(string Value)
        //{
        //    principalPaid = Value;
        //}

        //public void setBalance(string Value)
        //{
        //    balance = Value;
        //}

    }
    
#endregion

    /// <summary>
    /// 
    /// </summary>
    /// <remarks></remarks>
    public partial class InterestCalculator : PageBase
    {
        #region Global Variables
        /// <summary>
        /// 
        /// </summary>
        private double _totalInterest;
        /// <summary>
        /// 
        /// </summary>
        private double _totalPrincipal;
        /// <summary>
        /// 
        /// </summary>
        private double _totalPayment;
        #endregion

        #region Events
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsXsrf) { }
            else
            {
                if (!IsPostBack)
                {
                    Reset();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSubmit control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                
                double principal = Convert.ToDouble(txtAmountRs.Text);
                const double downpayment = 0;
                double duration = Convert.ToDouble(txtTerm.Text);// 3;// thirtyYearRB.Checked ? 30.0 : 15.0;
                double interestRate = Convert.ToDouble(txtAnnualInterestRate.Text);// 1;// Convert.ToDouble(interestRateEd.Text);

                // Calculate Monthly EMI 
                double amoutToBePaid = calculate_Amount(principal - downpayment, duration, interestRate);
                txtEmi.Text = amoutToBePaid.ToString("f2");
                //monthlyPaymented.Text = amoutToBePaid.ToString("f3");

                var list = BuildTree(principal - downpayment, duration, interestRate, amoutToBePaid);
                gridheader.Visible = true;
                grdEMIDetail.DataSource = list;
                grdEMIDetail.DataBind();
            }
            catch (Exception)
            {
                lblError.Text = Constants.GeneralErrorMessage;
            }
        }

        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        /// <remarks></remarks>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <remarks></remarks>
        private void Reset()
        {
            txtEmi.Text = "";
            txtAmountRs.Text = "";
            txtAnnualInterestRate.Text = "";
            txtTerm.Text = "";
            grdEMIDetail.DataSource = null;
            grdEMIDetail.DataBind();
            gridheader.Visible = false;
        }
        #endregion

#region EMI Calculator Logic
        /// <summary>
        /// Calculate_s the amount.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="duration">The duration.</param>
        /// <param name="interestRate">The interest rate.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double calculate_Amount(double principal, double duration, double interestRate)
        {
            try
            {
                double amttobepaid = 0;
                double irate = 0;
                irate = (interestRate / 100) * (1.0 / 12.0);
                amttobepaid = (principal * irate) / (1 - 1 / Math.Pow((1 + irate), duration));
                return amttobepaid;
            }
            catch (Exception)
            {
                //lblError.Text = exp.Message;
                return 0.0;
            }
        }
        /// <summary>
        /// Gets the interestamt.
        /// </summary>
        /// <param name="principal">The principal.</param>
        /// <param name="interest">The interest.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private double getInterestamt(double principal, double interest)
        {
            return principal * ((interest / 100) * (1.0 / 12.0));
        }


        /// <summary>
        /// Calculates the payments.
        /// </summary>
        /// <param name="pPrincipal">The p principal.</param>
        /// <param name="pDuration">Duration of the p.</param>
        /// <param name="interestRate">The interest rate.</param>
        /// <param name="monthlyPayment">The monthly payment.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.Collections.ArrayList CalculatePayments(double pPrincipal, double pDuration, double interestRate, double monthlyPayment)
        {
            double balanceamount = pPrincipal;
            double months = pDuration;

            var paymentList = new System.Collections.ArrayList();

            for (int count = 0; count <= pDuration - 1; count++)
            {
                MonthlyDetail monthlyDetail = null;
                if (count == 0)
                {
                    monthlyDetail = new MonthlyDetail();
                    monthlyDetail.Months = "0";
                    monthlyDetail.balance = "0";
                    monthlyDetail.cumulativePrinciple = "0";
                    monthlyDetail.principalPaid = "0";
                    monthlyDetail.cumulativeInterest = "0";
                    monthlyDetail.interestPaid = "0";
                    monthlyDetail.monthlyInterest = "0";
                    monthlyDetail.payment = "0";
                    monthlyDetail.cumulativePayment = "0";
                    monthlyDetail.balance = txtAmountRs.Text;
                    paymentList.Add(monthlyDetail);
                }
                double monthinterest = getInterestamt(balanceamount, interestRate);
                double monthprincipal = monthlyPayment - monthinterest;
                balanceamount = balanceamount - monthprincipal;
                monthlyDetail = new MonthlyDetail();
                monthlyDetail.Months = Convert.ToString(count+1);
                monthlyDetail.balance=balanceamount.ToString("f2");
                
                _totalPrincipal = _totalPrincipal + monthprincipal;
                monthlyDetail.cumulativePrinciple = _totalPrincipal.ToString("f2");

                monthlyDetail.principalPaid = monthprincipal.ToString("f2");

                _totalInterest = _totalInterest + monthinterest;
                monthlyDetail.cumulativeInterest = _totalInterest.ToString("f2");

                monthlyDetail.interestPaid = monthinterest.ToString("f2");
                monthlyDetail.monthlyInterest = monthinterest.ToString("f2");
                monthlyDetail.payment = txtEmi.Text;

                _totalPayment += monthlyPayment;
                monthlyDetail.cumulativePayment += _totalPayment.ToString("f2");

                paymentList.Add(monthlyDetail);
            }
            return paymentList;
        }

        /// <summary>
        /// Builds the tree.
        /// </summary>
        /// <param name="pPrincipal">The p principal.</param>
        /// <param name="pDuration">Duration of the p.</param>
        /// <param name="interestRate">The interest rate.</param>
        /// <param name="monthlyPayment">The monthly payment.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private System.Collections.ArrayList BuildTree(double pPrincipal, double pDuration, double interestRate, double monthlyPayment)
        {
            _totalInterest = 0;
            _totalPrincipal = 0;
            System.Collections.ArrayList paymentList = CalculatePayments(pPrincipal, pDuration, interestRate, monthlyPayment);
            return paymentList;
        }
        

#endregion
    }
}