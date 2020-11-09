import { Injectable } from '@angular/core';
import { AngularFireList, AngularFireDatabase } from '@angular/fire/database';

@Injectable({
  providedIn: 'root'
})
export class VideoService {


  private dbPath = '/videos';

  viedosRef: AngularFireList<Video> = null;

  constructor(private db: AngularFireDatabase) {
    this.viedosRef = db.list(this.dbPath);
  }

  getAll(): AngularFireList<Video> {
    return this.viedosRef;
  }

  create(video: Video): any {
    return this.db.list(this.dbPath).push(video);
  }

  update(key: string, value: any): Promise<void> {
    return this.viedosRef.update(key, value);
  }

  delete(key: string): Promise<void> {
    return this.viedosRef.remove(key);
  }

  deleteAll(): Promise<void> {
    return this.viedosRef.remove();
  }
}
export default class Video {
  key?: string;
  title: string;
  description: string;
  published = false;
  url: string
  type: string
  role_id? : number
}