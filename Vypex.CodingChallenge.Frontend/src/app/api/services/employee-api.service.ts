import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of, tap } from 'rxjs';
import { EmployeeDTO, EmployeeListDTO } from '../../interfaces/employeeDto';
import { environment } from './environment';

@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  private baseUrl = `${environment.apiBaseUrl}/employees`;
  constructor(private http: HttpClient) {}

  getEmployees(): Observable<EmployeeListDTO[]> {
    return this.http.get<EmployeeListDTO[]>(this.baseUrl).pipe(
      catchError((err) => {
        console.error('Failed to fetch employees:', err);
        return of([]);
      })
    );
  }

  getEmployeeDetails(employeeId: string): Observable<EmployeeDTO> {
    return this.http.get<EmployeeDTO>(`${this.baseUrl}/${employeeId}`).pipe(
      catchError((err) => {
        console.error('Failed to fetch employees:', err);
        return of();
      })
    );
  }

  updateEmployee(employee: EmployeeDTO): Observable<boolean> {
    const url = `${this.baseUrl}/${employee.employeeId}`;
    const body = {
      employeeId: employee.employeeId,
      leaves: employee.leaves,
    };

    return this.http.post<boolean>(url, body).pipe(
      tap(() =>
        console.log(`Updated leave details for employee ${employee.employeeId}`)
      ),
      catchError((err) => {
        console.error(`Failed to update employee ${employee.employeeId}:`, err);
        throw err;
      })
    );
  }
}
