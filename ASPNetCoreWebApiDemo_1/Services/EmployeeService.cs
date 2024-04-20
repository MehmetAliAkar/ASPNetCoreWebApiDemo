using ASPNetCoreWebApiDemo_1.Models;
using ASPNetCoreWebApiDemo_1.ViewModels;

namespace ASPNetCoreWebApiDemo_1.Services
{
    public class EmployeeService : IEmployeeService
    {
        private EmpContext _context;

        public EmployeeService(EmpContext context)
        {
            _context = context;
        }

        public List<Employees> GetEmployeesList()
        {
            List<Employees> empList;
            try
            {
                empList = _context.Set<Employees>().ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return empList;
        }

        public Employees GetEmployeeDetailsById(int empId)
        {
            Employees emp;
            try
            {
                emp = _context.Find<Employees>(empId);
            }
            catch (Exception)
            {
                throw;
            }
            return emp;
        }

        public ResponseModel SaveEmployee(Employees employeeModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Employees _temp = GetEmployeeDetailsById(employeeModel.EmployeeId);
                if (_temp != null)
                {
                    _temp.Designation = employeeModel.Designation;
                    _temp.EmployeeFirstName = employeeModel.EmployeeFirstName;
                    _temp.EmployeeLastName = employeeModel.EmployeeLastName;
                    _temp.Salary = employeeModel.Salary;
                    _context.Update<Employees>(_temp);
                    model.Message = "Employee Update Successfully";
                }
                else
                {
                    _context.Add<Employees>(employeeModel);
                    model.Message = "Employee Inserted Successfully";
                }
                _context.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel DeleteEmployee(int employeeId)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                Employees _temp = GetEmployeeDetailsById(employeeId);
                if (_temp != null)
                {
                    _context.Remove<Employees>(_temp);
                    _context.SaveChanges();
                    model.IsSuccess = true;
                    model.Message = "Employee Deleted Successfully";
                }
                else
                {
                    model.IsSuccess = false;
                    model.Message = "Employee Not Found";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
