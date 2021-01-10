import { Component, OnInit, AfterContentInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { ICandidatePhoto } from '../../shared/models/CandidatePhoto';
import { HrManagerService } from '../hr-manager.service';
import { AccountService } from 'src/app/account/account.service';
import swal from 'sweetalert2';

@Component({
  selector: 'app-hr-manage-profile-photo',
  templateUrl: './hr-manage-profile-photo.component.html',
  styleUrls: ['./hr-manage-profile-photo.component.css']
})
export class HrManageProfilePhotoComponent implements OnInit, AfterContentInit {
  files: File[] = [];
  candidatePhotos$: Observable<ICandidatePhoto[]>;
  message: string;
  deletePhoto: ICandidatePhoto;
  setMainPhoto: ICandidatePhoto;
  candidateId: string;
  constructor(
    private candidateService: HrManagerService,
    private route: ActivatedRoute) { }

  onSelect(event) {
    console.log('seleted file', event);
    this.files.push(...event.addedFiles);
    this.onFileUpload();
    }
    ngAfterContentInit(): void {
      this.candidateService.getCandidatePhotos(+ this.candidateId).subscribe();
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
      this.candidateId = this.route.snapshot.paramMap.get('id');
      this.candidatePhotos$ = this.candidateService.candidatePhotos$;
    }
    onFileUpload() {
      const fd = new FormData();
      fd.append('File', this.files[0], this.files[0].name);
      fd.append('candidateId', this.candidateId);
      this.candidateService.uploadCandidatePhoto(fd)
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
        this.candidateService.setCandidatePhotoMain(item.id, + this.candidateId).subscribe(() => {
          this.setMainPhoto.isMain = true;
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
