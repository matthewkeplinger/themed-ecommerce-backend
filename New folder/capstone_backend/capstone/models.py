from django.db import models



# Create your models here.




class Song(models.Model):
    title = models.CharField(max_length=50)
    artist = models.CharField(max_length=50)
    album = models.CharField(max_length=50)
    release_date = models.DateTimeField()
    genre = models.CharField(max_length=50)
    likes = models.IntegerField(default=0)
    Song_id= models.TextField(blank=True, null=True, max_length=500)
    
    def __str__(self):
        return self.Song



class CommentSection(models.Model):
    comment= models.TextField(max_length=200)   
   
    like= models.IntegerField(default= None, blank=True, null=True)
    dislike= models.IntegerField(blank=True, null=True)
    songcomment = models.ForeignKey(Song ,blank=True, null=True,  on_delete=models.CASCADE)

    def __str__(self):
        return self.comment
    

