import { Component, OnInit, AfterContentInit, TemplateRef } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICandidatePhoto } from '../../shared/models/CandidatePhoto';
import { CandidateService } from '../candidates.service';
import { AccountService } from 'src/app/account/account.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-upload-photo',
  templateUrl: './upload-photo.component.html',
  styleUrls: ['./upload-photo.component.css']
})
export class UploadPhotoComponent implements OnInit, AfterContentInit {
 files: File[] = [];
 candidatePhotos$: Observable<ICandidatePhoto[]>;
 message: string;
 deletePhoto: ICandidatePhoto;
 setMainPhoto: ICandidatePhoto;

 constructor(
   private http: HttpClient,
   private accountService: AccountService,
   private candidateService: CandidateService) { }

  onSelect(event) {
  console.log('seleted file', event);
  this.files.push(...event.addedFiles);
  this.onFileUpload();

  }
  ngAfterContentInit(): void {
    this.candidateService.getCandidatePhotos().subscribe();
  }
  refreshPhotos(item: ICandidatePhoto){
    this.candidateService.updateAfterInsert(item);
  }
  onRemove(event) {
    console.log('remove', event);
    console.log(this.files.indexOf(event));
    this.files.splice(this.files.indexOf(event), 1);
  }
  onRemove1() {
    this.files.splice(0, 1);
  }
  ngOnInit(): void {
    this.candidatePhotos$ = this.candidateService.candidatePhotos$;
  }
  onFileUpload() {
    const fd = new FormData();
    fd.append('File', this.files[0], this.files[0].name);

    this.http.post('https://localhost:44327/api/CandidatePhoto', fd)
      .subscribe((res: ICandidatePhoto) =>  {
      console.log(res);
      this.onRemove1();
      this.refreshPhotos(res);
    }
    );
  }
 async setToMainPicture(item: ICandidatePhoto ) {
    this.setMainPhoto = item;
    const result = await swal.fire({
      title: 'Are you sure you?',
      text: 'This Photo will set as main !',
      icon: 'info',
      showCancelButton: true,
      confirmButtonText: 'Yes, set as default!',
      cancelButtonText: 'No'
    });
    if (result.value)
    {
      console.log(result.value);
      this.candidateService.setCandidatePhotoMain(item.id).subscribe(() => {
        this.setMainPhoto.isMain = true;
        this.accountService.changeMemberPhoto(item.url);
        swal.fire(
          'Changed!',
          'image has been set as default picture.',
          'success'
          );
      });
    } else if (result.dismiss === swal.DismissReason.cancel)
    {
      swal.fire(
          'Cancelled',
          'everything remains the same :)',
          'info'
        );
    }
  }

  updatePhoto(item: ICandidatePhoto) {
   this.candidateService.UpdateCandidatePhoto(item.id, 'today');
  }
  async showModateswal(item: ICandidatePhoto) {
    this.deletePhoto = item;
    const result = await swal.fire({
      title: 'Are you sure?',
      text: 'You will not be able to recover this imaginary file!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    });
    if (result.value)
    {
      console.log(result.value);
      this.candidateService.deleteCandidatePhoto(this.deletePhoto.id).subscribe(() => {
        swal.fire(
        'Deleted!',
        'Your imaginary file has been deleted.',
        'success'
        );
      });

    } else if (result.dismiss === swal.DismissReason.cancel)
    {
      swal.fire(
          'Cancelled',
          'Your imaginary file is safe :)',
          'error'
        );
    }
  }
}
