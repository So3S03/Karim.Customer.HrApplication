using Karim.Customer.HrApplication.Domain.Entities.Identity;

namespace Karim.Customer.HrApplication.Infrastructure.Persistence.Data.Privilages
{
    public static class PrivList
    {
        public static List<AppPrivilages> GeneratePrivilages()
        {
            var privs = new List<AppPrivilages>()
            {
                //Admin
                new AppPrivilages() { Id = "1", Name = "Admin", NormalizedName = "ADMIN", PrivNumber = 0000 },
                //Dashboard
                new AppPrivilages() { Id = "2", Name = "Dashboard", NormalizedName = "DASHBOARD", PrivNumber = 1000 },
                new AppPrivilages() { Id = "3", Name = "Dashboard.Summary", NormalizedName = "DASHBOARD.SUMMARY", PrivNumber = 1110 },
                new AppPrivilages() { Id = "4", Name = "Dashboard.Attendance_Rate", NormalizedName = "DASHBOARD.ATTENDANCE_RATE", PrivNumber = 1201 },
                new AppPrivilages() { Id = "5", Name = "Dashboard.Payroll_Rate", NormalizedName = "DASHBOARD.PAYROLL_RATE", PrivNumber = 1202 },
                new AppPrivilages() { Id = "6", Name = "Dashboard.Departments", NormalizedName = "DASHBOARD.DEPARTMENTS", PrivNumber = 1203 },
                new AppPrivilages() { Id = "7", Name = "Dashboard.Employees", NormalizedName = "DASHBOARD.EMPLOYEES", PrivNumber = 1204 },
                new AppPrivilages() { Id = "8", Name = "Dashboard.Employee_Performance_Rate", NormalizedName = "DASHBOARD.EMPLOYEE_PERFORMANCE_RATE", PrivNumber = 1205 },
                new AppPrivilages() { Id = "9", Name = "Dashboard.Employee_NewHire_VS_Terminations_Rate", NormalizedName = "DASHBOARD.EMPLOYEE_NEWHIRES_VS_TERMINATIONS_RATE", PrivNumber = 1206 },
                //Employee
                new AppPrivilages() { Id = "10", Name = "Employee", NormalizedName = "EMPLOYEE", PrivNumber = 2000 },
                new AppPrivilages() { Id = "11", Name = "Employee.Grid", NormalizedName = "EMPLOYEE.GRID", PrivNumber = 2100 },
                new AppPrivilages() { Id = "12", Name = "Employee.Add_Employee", NormalizedName = "EMPLOYEE.ADD_EMPLOYEE", PrivNumber = 2101 },
                new AppPrivilages() { Id = "13", Name = "Employee.Update_Employee", NormalizedName = "EMPLOYEE.UPDATE_EMPLOYEE", PrivNumber = 2102 },
                new AppPrivilages() { Id = "14", Name = "Employee.Remove_Employee_Temp", NormalizedName = "EMPLOYEE.REMOVE_EMPLOYEE_TEMP", PrivNumber = 2103 },
                new AppPrivilages() { Id = "15", Name = "Employee.Remove_Employee_Perm", NormalizedName = "EMPLOYEE.REMOVE_EMPLOYEE_PERM", PrivNumber = 2104 },
                new AppPrivilages() { Id = "16", Name = "Employee.Retore_Employee", NormalizedName = "EMPLOYEE.RESTORE_EMPLOYEE", PrivNumber = 2105 },
                new AppPrivilages() { Id = "17", Name = "Employee.Upload_Employee_Photo", NormalizedName = "EMPLOYEE.UPLOAD_EMPLOYEE_PHOTO", PrivNumber = 2106 },
                new AppPrivilages() { Id = "18", Name = "Employee.Remove_Employee_Photo", NormalizedName = "EMPLOYEE.REMOVE_EMPLOYEE_PHOTO", PrivNumber = 2107 },
                new AppPrivilages() { Id = "19", Name = "Employee.Terminate_Employee", NormalizedName = "EMPLOYEE.TERMINATE_EMPLOYEE", PrivNumber = 2108 },
                new AppPrivilages() { Id = "20", Name = "Employee.Undo_Terminate_Employee", NormalizedName = "EMPLOYEE.UNDO_TERMINATE_EMPLOYEE", PrivNumber = 2109 },
                new AppPrivilages() { Id = "21", Name = "Employee.Print_Employee_ID", NormalizedName = "EMPLOYEE.PRINT_EMPLOYEE_ID", PrivNumber = 21010 },
                new AppPrivilages() { Id = "22", Name = "Employee.Terminate_Collective_Employee", NormalizedName = "EMPLOYEE.TERMINATE_COLLECTIVE_EMPLOYEE", PrivNumber = 2201 },
                new AppPrivilages() { Id = "23", Name = "Employee.Undo_Terminate_Collective_Employee", NormalizedName = "EMPLOYEE.UNDO_TERMINATE_COLLECTIVE_EMPLOYEE", PrivNumber = 2202 },
                new AppPrivilages() { Id = "24", Name = "Employee.Add_Bulk_Employee", NormalizedName = "EMPLOYEE.ADD_BULK_EMPLOYEE", PrivNumber = 2203 },
                new AppPrivilages() { Id = "25", Name = "Employee.Reports", NormalizedName = "EMPLOYEE.REPORTS", PrivNumber = 2300 },
                new AppPrivilages() { Id = "26", Name = "Employee.Attendance_Employee_Report", NormalizedName = "EMPLOYEE.ATTENDANCE_EMPLOYEE_REPORT", PrivNumber = 2301 },
                new AppPrivilages() { Id = "27", Name = "Employee.Employee_Performance_Report", NormalizedName = "EMPLOYEE.EMPLOYEE_PERFORMANCE_REPORT", PrivNumber = 2302 },
                new AppPrivilages() { Id = "28", Name = "Employee.Employee_Payroll_Report", NormalizedName = "EMPLOYEE.EMPLOYEE_PAYROLL_REPORT", PrivNumber = 2303 },
                //Department
                new AppPrivilages() { Id = "29", Name = "Department", NormalizedName = "DEPARTMENT", PrivNumber = 3000 },
                new AppPrivilages() { Id = "30", Name = "Department.Grid", NormalizedName = "DEPARTMENT.GRID", PrivNumber = 3100 },
                new AppPrivilages() { Id = "31", Name = "Department.Add_Department", NormalizedName = "DEPARTMENT.ADD_DEPARTMENT", PrivNumber = 3101 },
                new AppPrivilages() { Id = "32", Name = "Department.Edit_Department", NormalizedName = "DEPARTMENT.EDIT_DEPARTMENT", PrivNumber = 3102 },
                new AppPrivilages() { Id = "33", Name = "Department.Add_Department_Member", NormalizedName = "DEPARTMENT.ADD_DEPARTMENT_MEMBER", PrivNumber = 3103 },
                new AppPrivilages() { Id = "34", Name = "Department.Add_Department_Manager", NormalizedName = "DEPARTMENT.ADD_DEPARTMENT_MANAGER", PrivNumber = 3104 },
                new AppPrivilages() { Id = "35", Name = "Department.ActivateDe_Department", NormalizedName = "DEPARTMENT.ACTIVATEDE_DEPARTMENT", PrivNumber = 3105 },
                new AppPrivilages() { Id = "36", Name = "Department.Reports", NormalizedName = "DEPARTMENT.REPORTS", PrivNumber = 3300 },
                new AppPrivilages() { Id = "37", Name = "Department.Department_Performance_Reports", NormalizedName = "DEPARTMENT.DEPARTMENT_PERFORMANCE_REPORT", PrivNumber = 3301 },
                new AppPrivilages() { Id = "38", Name = "Department.Department_Budget_Analysis_Reports", NormalizedName = "DEPARTMENT.DEPARTMENT_BUDGET_ANALYSIS_REPORT", PrivNumber = 3302 },
                new AppPrivilages() { Id = "39", Name = "Department.Department_Activity_Reports", NormalizedName = "DEPARTMENT.DEPARTMENT_ACTIVITY_REPORT", PrivNumber = 3303 },
                //Attendance
                new AppPrivilages() { Id = "40", Name = "Attendance", NormalizedName = "ATTENDANCE", PrivNumber = 4000 },
                new AppPrivilages() { Id = "41", Name = "Attendance.Grid_Calendar", NormalizedName = "ATTENDANCE.GRID_CALENDAR", PrivNumber = 4100 },
                new AppPrivilages() { Id = "42", Name = "Attendance.Summary", NormalizedName = "ATTENDANCE.SUMMARY", PrivNumber = 4010 },
                new AppPrivilages() { Id = "43", Name = "Attendance.Requests", NormalizedName = "ATTENDANCE.REQUESTS", PrivNumber = 4101 },
                new AppPrivilages() { Id = "44", Name = "Attendance.Admin_Fingerprint", NormalizedName = "ATTENDANCE.ADMIN_FINGERPRINT", PrivNumber = 4201 },
                new AppPrivilages() { Id = "45", Name = "Attendance.Employee_Checkin", NormalizedName = "ATTENDANCE.EMPLOYEE_CHECKIN", PrivNumber = 4202 },
                new AppPrivilages() { Id = "46", Name = "Attendance.Bulk_Employees_Checkin", NormalizedName = "ATTENDANCE.BULK_EMPLOYEE_CHECKIN", PrivNumber = 4203 },
                new AppPrivilages() { Id = "47", Name = "Attendance.Reports", NormalizedName = "ATTENDANCE.REPORTS", PrivNumber = 4300 },
                new AppPrivilages() { Id = "48", Name = "Attendance.Monthly_Attendance_Reports", NormalizedName = "ATTENDANCE.MONTHLY_ATTENDANCE_REPORTS", PrivNumber = 4301 },
                new AppPrivilages() { Id = "49", Name = "Attendance.Late_Arrives_Reports", NormalizedName = "ATTENDANCE.LATE_ARRIVES_REPORTS", PrivNumber = 4302 },
                new AppPrivilages() { Id = "50", Name = "Attendance.Monthly_Leaves_Reports", NormalizedName = "ATTENDANCE.MONTHLY_LEAVES_REPORTS", PrivNumber = 4303 },
                //Payroll
                new AppPrivilages() { Id = "51", Name = "Payroll", NormalizedName = "PAYROLL", PrivNumber = 5000 },
                new AppPrivilages() { Id = "52", Name = "Payroll.Summary", NormalizedName = "PAYROLL.SUMMARY", PrivNumber = 5010 },
                new AppPrivilages() { Id = "53", Name = "Payroll.Grid", NormalizedName = "PAYROLL.GIRD", PrivNumber = 5100 },
                new AppPrivilages() { Id = "54", Name = "Payroll.Add_Payroll", NormalizedName = "PAYROLL.ADD_PAYROLL", PrivNumber = 5101 },
                new AppPrivilages() { Id = "55", Name = "Payroll.Edit_Payroll", NormalizedName = "PAYROLL.EDIT_PAYROLL", PrivNumber = 5102 },
                new AppPrivilages() { Id = "56", Name = "Payroll.Add_Payroll_Bonus", NormalizedName = "PAYROLL.ADD_PAYROLL_BONUS", PrivNumber = 5103 },
                new AppPrivilages() { Id = "57", Name = "Payroll.Add_Payroll_Deductions", NormalizedName = "PAYROLL.ADD_PAYROLL_DEDUCTIONS", PrivNumber = 5104 },
                new AppPrivilages() { Id = "58", Name = "Payroll.Reports", NormalizedName = "PAYROLL.REPORTS", PrivNumber = 5300 },
                new AppPrivilages() { Id = "59", Name = "Payroll.Payroll_Summary_Reports", NormalizedName = "PAYROLL.PAYROLL_SUMMARY_REPORTS", PrivNumber = 5301 },
                new AppPrivilages() { Id = "60", Name = "Payroll.Tax_Reports", NormalizedName = "PAYROLL.TAX_REPORTS", PrivNumber = 5302 },
                new AppPrivilages() { Id = "61", Name = "Payroll.Bonus_Reports", NormalizedName = "PAYROLL.BONUS_REPORTS", PrivNumber = 5303 },
                //Organisation Chart
                new AppPrivilages() { Id = "62", Name = "Org_Chart", NormalizedName = "ORG_CHART", PrivNumber = 6000 },
                new AppPrivilages() { Id = "63", Name = "Org_Chart_Org_Structure", NormalizedName = "ORG_CHART_ORG_STRUCTURE", PrivNumber = 6100 },
                //Projects
                new AppPrivilages() { Id = "64", Name = "Project", NormalizedName = "PROJECT", PrivNumber = 9000 },
                new AppPrivilages() { Id = "65", Name = "Project.Grid", NormalizedName = "PROJECT.GRID", PrivNumber = 9100 },
                new AppPrivilages() { Id = "66", Name = "Project.Add_Project", NormalizedName = "PROJECT.ADD_PROJECT", PrivNumber = 9101 },
                new AppPrivilages() { Id = "67", Name = "Project.Edit_Project", NormalizedName = "PROJECT.EDIT_PROJECT", PrivNumber = 9102 },
                new AppPrivilages() { Id = "68", Name = "Project.Delete_Project", NormalizedName = "PROJECT.DELETE_PROJECT", PrivNumber = 9103 },
                new AppPrivilages() { Id = "69", Name = "Project.Manage_Project_Tasks", NormalizedName = "PROJECT.MANAGE_PROJECT_TASKS", PrivNumber = 9104 },
                new AppPrivilages() { Id = "70", Name = "Project.Assign_Project_Manager", NormalizedName = "PROJECT.ASSIGN_PROJECT_MANAGER", PrivNumber = 9105 },
                new AppPrivilages() { Id = "71", Name = "Project.Reports", NormalizedName = "PROJECT.REPORTS", PrivNumber = 9300 },
                new AppPrivilages() { Id = "72", Name = "Project.Project_Progress_Reports", NormalizedName = "PROJECT.PROJECT_PROGRESS_REPORTS", PrivNumber = 9301 },
                new AppPrivilages() { Id = "73", Name = "Project.Resource_Allocation", NormalizedName = "PROJECT.RESOURCE_ALLOCATION", PrivNumber = 9302 },
                new AppPrivilages() { Id = "74", Name = "Project.Timeline_Analysis", NormalizedName = "PROJECT.TIMELINE_ANALYSIS", PrivNumber = 9303 },
                //Tasks
                new AppPrivilages() { Id = "75", Name = "Tasks", NormalizedName = "TASKS", PrivNumber = 7000 },
                new AppPrivilages() { Id = "76", Name = "Tasks.Grid", NormalizedName = "TASKS.GRID", PrivNumber = 7100 },
                new AppPrivilages() { Id = "77", Name = "Tasks.Summary", NormalizedName = "TASKS.SUMMARY", PrivNumber = 7010 },
                new AppPrivilages() { Id = "78", Name = "Tasks.Add_Task", NormalizedName = "TASKS.ADD_TASK", PrivNumber = 7101 },
                new AppPrivilages() { Id = "79", Name = "Tasks.Edit_Task", NormalizedName = "TASKS.EDIT_TASK", PrivNumber = 7102 },
                new AppPrivilages() { Id = "80", Name = "Tasks.Archive_Task", NormalizedName = "TASKS.ARCHIVE_TASK", PrivNumber = 7103 },
                new AppPrivilages() { Id = "81", Name = "Tasks.Delete_Task", NormalizedName = "TASKS.DELETE_TASK", PrivNumber = 7104 },
                new AppPrivilages() { Id = "82", Name = "Tasks.Shift_Employee_Task", NormalizedName = "TASKS.SHIFT_EMPLOYEE_TASK", PrivNumber = 7105 },
                //Contract
                new AppPrivilages() { Id = "83", Name = "Contract", NormalizedName = "CONTRACT", PrivNumber = 8000 },
                new AppPrivilages() { Id = "84", Name = "Contract.Summary", NormalizedName = "CONTRACT.SUMMARY", PrivNumber = 8010 },
                new AppPrivilages() { Id = "85", Name = "Contract.Grid", NormalizedName = "CONTRACT.GRID", PrivNumber = 8101 },
                new AppPrivilages() { Id = "86", Name = "Contract.Add_Contract", NormalizedName = "CONTRACT.ADD_CONTRACT", PrivNumber = 8102 },
                new AppPrivilages() { Id = "87", Name = "Contract.Edit_Contract", NormalizedName = "CONTRACT.EDIT_CONTRACT", PrivNumber = 8103 },
                new AppPrivilages() { Id = "88", Name = "Contract.Delete_Contract", NormalizedName = "CONTRACT.DELETE_CONTRACT", PrivNumber = 8104 },
                new AppPrivilages() { Id = "89", Name = "Contract.Print_Contract", NormalizedName = "CONTRACT.PRINT_CONTRACT", PrivNumber = 8105 },
                new AppPrivilages() { Id = "90", Name = "Contract.DeActivate_Contract", NormalizedName = "CONTRACT.DEACTIVATE_CONTRACT", PrivNumber = 8106 },
                new AppPrivilages() { Id = "91", Name = "Contract.Reports", NormalizedName = "CONTRACT.REPORTS", PrivNumber = 8300 },
                new AppPrivilages() { Id = "92", Name = "Contract.Contract_Status_Reports", NormalizedName = "CONTRACT.CONTRACT_STATUS_REPORTS", PrivNumber = 8301 },
                new AppPrivilages() { Id = "93", Name = "Contract.Contract_Renewal_Report", NormalizedName = "CONTRACT.CONTRACT_RENEWAL_REPORT", PrivNumber = 8302 },
                new AppPrivilages() { Id = "94", Name = "Contract.Contract_Analyics_Report", NormalizedName = "CONTRACT.CONTRACT_ANALYTICS_REPORT", PrivNumber = 8303 },
            };
            return privs;
        }
    }
}