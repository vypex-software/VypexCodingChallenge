import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Project } from '../model';
import { ProjectSummary } from '../model/project-summary';
import { ProjectUpdate } from '../model/project-update';

@Injectable({ providedIn: 'root' })
export class ProjectApiService {
  private readonly httpClient = inject(HttpClient);

  private readonly baseUrl = 'https://localhost:7189/api';

  public getProjects(search?: string | null): Observable<Array<ProjectSummary>> {
    let params = new HttpParams();

    if (search) {
      params = params.set('search', search);
    }

    return this.httpClient.get<Array<ProjectSummary>>(`${this.baseUrl}/projects`, { params });
  }

  public getProject(id: number): Observable<Project | null> {
    return this.httpClient.get<Project | null>(`${this.baseUrl}/projects/${id}`);
  }

  public updateProject(id: number, update: ProjectUpdate): Observable<Project | null> {
    return this.httpClient.patch<Project | null>(`${this.baseUrl}/projects/${id}`, update);
  }
}
