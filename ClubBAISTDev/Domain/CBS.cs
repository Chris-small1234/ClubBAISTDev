using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubBAISTDev.TechnicalServices;

namespace ClubBAISTDev.Domain
{
    public class CBS
    {
        public bool Login(int memberId, string memberPassword)
        {
            bool Success = false;
            Members MemberManager = new();
            Success = MemberManager.Login(memberId, memberPassword);

            return Success;
        }

        public DailyTeeSheet GetDailyTeeSheet(DateTime selectedDate)
        {
            DailyTeeSheets DailyTeeSheetManager = new();
            DailyTeeSheet TeeSheet = new();
            TeeSheet = DailyTeeSheetManager.GetDailyTeeSheet(selectedDate);

            return TeeSheet;
        }

        public List<TeeTime> GetTeeTimes(int dailyTeeSheetId)
        {
            TeeTimes TeeTimeManager = new();
            Players PlayersManager = new();
            
            List<TeeTime> TodayTeeTimes = new();
            TodayTeeTimes = TeeTimeManager.GetTeeTimes(dailyTeeSheetId);
            foreach(TeeTime TodayTeeTime in TodayTeeTimes)
            {
                TodayTeeTime.Players = PlayersManager.GetPlayers(TodayTeeTime.TeeTimeId);
            }
            return TodayTeeTimes;
        }

        public ReturnItem CreateTeeTime(int NumberOfPlayersField, string PhoneField, int NumberOfCartsField, DateTime TeeTimeField, string EmployeeNameField, int MemberIdField, int DailyTeeSheetIdField, List<Player> CurrentPlayers)
        {
            Members MemberManager = new();
            Member CurrentMember = MemberManager.GetMember(MemberIdField);

            Players PlayerManager = new();

            TeeTimes TeeTimeManager = new();
            ReturnItem Return = new();
            TeeTime ExistingTeeTime = TeeTimeManager.GetTeeTime(TeeTimeField);
            // Check for exsiting tee time
            if (ExistingTeeTime.TeeTimeId == 0)
            {
                TeeTime RequestedTeeTime = new()
                {
                    NumberOfPlayers = NumberOfPlayersField,
                    Phone = PhoneField,
                    NumberOfCarts = NumberOfCartsField,
                    SetTeeTime = TeeTimeField,
                    EmployeeName = EmployeeNameField,
                    MemberId = MemberIdField,
                    DailyTeeSheetId = DailyTeeSheetIdField
                };
                DateTime Today = DateTime.Today;
                RequestedTeeTime.NumberOfPlayers = CurrentPlayers.Count();
                switch (CurrentMember.MembershipLevel)
                {
                    // Check membership level
                    case "G":
                        Return.Result = TeeTimeManager.CreateTeeTime(RequestedTeeTime);
                        Return.Message = "Tee Time Successfully Scheduled!";
                        break;

                    case "S":
                        // Check for DayOfWeek
                        if (RequestedTeeTime.SetTeeTime.DayOfWeek.ToString() == "Saturday" || RequestedTeeTime.SetTeeTime.DayOfWeek.ToString() == "Sunday")
                        {
                            // Check for Time based on day
                            DateTime eleven = new DateTime(Today.Year, Today.Month, Today.Day, 11, 0, 0);
                            if (RequestedTeeTime.SetTeeTime.TimeOfDay > eleven.TimeOfDay)
                            {
                                Return.Result = TeeTimeManager.CreateTeeTime(RequestedTeeTime);
                                Return.Message = "Tee Time Successfully Scheduled!";
                            } else
                            {
                                Return.Result = false;
                                Return.Message = "Silver members cannot schedule tee time before 11 AM on Weekends";
                            }
                        } else {
                            // Check for Time based on day
                            DateTime three = new DateTime(Today.Year, Today.Month, Today.Day, 15, 0, 0);
                            DateTime fivethirty = new DateTime(Today.Year, Today.Month, Today.Day, 17, 30, 0);
                            if (RequestedTeeTime.SetTeeTime.TimeOfDay < three.TimeOfDay || RequestedTeeTime.SetTeeTime.TimeOfDay > fivethirty.TimeOfDay)
                            {
                                Return.Result = TeeTimeManager.CreateTeeTime(RequestedTeeTime);
                                Return.Message = "Tee Time Successfully Scheduled!";
                            }
                            else
                            {
                                Return.Result = false;
                                Return.Message = "Silver members cannot schedule tee time after 3:00 PM and before 5:30 PM on weekdays";
                            }
                        }
                        break;

                    case "B":
                        // Check for DayOfWeek
                        if (RequestedTeeTime.SetTeeTime.DayOfWeek.ToString() == "Saturday" || RequestedTeeTime.SetTeeTime.DayOfWeek.ToString() == "Sunday")
                        {
                            // Check for Time based on day
                            DateTime one = new DateTime(Today.Year, Today.Month, Today.Day, 13, 0, 0);
                            if (RequestedTeeTime.SetTeeTime.TimeOfDay > one.TimeOfDay)
                            {
                                Return.Result = TeeTimeManager.CreateTeeTime(RequestedTeeTime);
                                Return.Message = "Tee Time Successfully Scheduled!";
                            }
                            else
                            {
                                Return.Result = false;
                                Return.Message = "Bronze members cannot schedule tee time before 11 AM on weekends";
                            }
                        }
                        else
                        {
                            // Check for Time based on day
                            DateTime three = new DateTime(Today.Year, Today.Month, Today.Day, 15, 0, 0);
                            DateTime six = new DateTime(Today.Year, Today.Month, Today.Day, 18, 0, 0);
                            if (RequestedTeeTime.SetTeeTime.TimeOfDay < three.TimeOfDay || RequestedTeeTime.SetTeeTime.TimeOfDay > six.TimeOfDay)
                            {
                                Return.Result = TeeTimeManager.CreateTeeTime(RequestedTeeTime);
                                Return.Message = "Tee Time Successfully Scheduled!";
                            }
                            else
                            {
                                Return.Result = false;
                                Return.Message = "Bronze members cannot schedule tee time after 3:00 PM and before 6:00 PM on weekdays";
                            }
                        }
                        break;

                    case "C":
                        Return.Result = false;
                        Return.Message = "Copper members cannot schedule tee times";
                        break;

                    default:
                        Return.Result = false;
                        Return.Message = "Something happened, please try again";
                        break;
                }
            } else
            {
                Return.Result = false;
                Return.Message = "There is an existing tee time booked at that time, please choose a different time or day";
            }
            if (Return.Result == true)
            {
                int TeeTimeId = TeeTimeManager.GetNewTeeTimeId(MemberIdField, TeeTimeField);
                foreach(Player player in CurrentPlayers)
                {
                    if (player.PlayerName != null)
                    {
                        Return.Result = PlayerManager.CreatePlayer(TeeTimeId, player.MemberId, player.PlayerName);
                        if (Return.Result == false)
                        {
                            Return.Message = "Error creating Player";
                            break;
                        }
                    }
                }
            }
            return Return;
        }

        public bool CreateTeeSheet(DateTime TeeSheetDate, string TeeSheetDayOfWeek)
        {
            DailyTeeSheets DailyTeeSheetManager = new();
            bool Confirmation;
            Confirmation = DailyTeeSheetManager.CreateDailyTeeSheet(TeeSheetDate, TeeSheetDayOfWeek);

            return Confirmation;
        }

        public bool CreateStandingTeeTimeRequest(int MemberId, DateTime RequestedTeeTimeField, string DayOfWeekField, DateTime StartDateField, DateTime EndDateField, bool ApprovedField, string Player1Name, string Player2Name, string Player3Name, string Player4Name)
        {
            StandingTeeTimeRequest RequestedStandingTeeTime = new()
            {
                MemberId = MemberId,
                RequestedTeeTime = RequestedTeeTimeField,
                DayOfWeek = DayOfWeekField,
                StartDate = StartDateField,
                EndDate = EndDateField,
                Approved = ApprovedField,
                Player1Name = Player1Name,
                Player2Name = Player2Name,
                Player3Name = Player3Name,
                Player4Name = Player4Name
            };
            StandingTeeTimeRequests StandingTeeTimeManager = new();
            bool Confirmation;
            Confirmation = StandingTeeTimeManager.CreateStandingTeeTimeRequest(RequestedStandingTeeTime);

            return Confirmation;
        }

        public List<Member> GetMembers()
        {
            Members MemberManager = new();
            List<Member> AllMembers = new();

            AllMembers = MemberManager.GetMembers();

            return AllMembers;
        }

        public Member GetMember(int memberId)
        {
            Members MemberManager = new();
            Member CurrentMember = MemberManager.GetMember(memberId);

            return CurrentMember;
        }

        public bool CancelStandingTeeTimeRequest(int memberId)
        {
            bool Confirmation;

            StandingTeeTimeRequests StandingTeeTimeRequestManager = new();

            Confirmation = StandingTeeTimeRequestManager.CancelStandingTeeTimeRequest(memberId);

            return Confirmation;
        }

        public List<StandingTeeTimeRequest> GetStandingTeeTimeRequests()
        {
            List<StandingTeeTimeRequest> AllStandingTeeTimeRequests;

            StandingTeeTimeRequests StandingTeeTimeRequestManager = new();

            AllStandingTeeTimeRequests = StandingTeeTimeRequestManager.GetStandingTeeTimeRequests();

            return AllStandingTeeTimeRequests;
        }

        public StandingTeeTimeRequest GetStandingTeeTimeRequestByMemberId(int memberId)
        {
            StandingTeeTimeRequest CurrentStandingTeeTimeRequest;

            StandingTeeTimeRequests StandingTeeTimeRequestManager = new();

            CurrentStandingTeeTimeRequest = StandingTeeTimeRequestManager.GetStandingTeeTimeRequestByMemberId(memberId);

            return CurrentStandingTeeTimeRequest;
        }

        public List<Charge> GetChargesByMember(int memberId)
        {
            List<Charge> AllCharges;

            Charges ChargeManager = new();

            AllCharges = ChargeManager.GetChargesByMember(memberId);

            List<Charge> AllRoundedCharges = new();

            foreach(Charge element in AllCharges)
            {
                Charge RoundedCharge = new();
                RoundedCharge.ChargeId = element.ChargeId;
                RoundedCharge.Amount = Math.Round(element.Amount, 2);
                RoundedCharge.WhenCharged = element.WhenCharged;
                RoundedCharge.WhenBooked = element.WhenBooked;
                AllRoundedCharges.Add(RoundedCharge);
            }

            return AllRoundedCharges;
        }
    }
}
