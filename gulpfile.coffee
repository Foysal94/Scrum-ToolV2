gulp = require 'gulp'
rimraf = require 'rimraf'
concat = require 'gulp-concat'
cssmin = require 'gulp-cssmin'
uglify = require 'gulp-uglify'
project = require './project.json'
sass = require 'gulp-sass'
coffee = require 'gulp-coffee'
gutil = require 'gulp-util' 
rename = require 'gulp-rename'

paths = 
  webroot: "./wwwroot/"
  js: './wwwroot/js/**/*.js'
  minJs: './wwwroot/js/**/*.min.js'
  css: './wwwroot/css/**/*.css'
  minCss: 'css/**/*.min.css'
  scss: './assets/scss/**/*.scss'
  coffee: './assets/coffee/**/*.coffee'
  
  
gulp.task 'sass-css', ->
    return gulp.src paths.scss
           .pipe sass()
           .pipe gulp.dest paths.webroot + 'css/'
           
 

gulp.task 'coffee-js', ->
    return gulp.src paths.coffee
          .pipe(coffee({bare: true}).on 'error', (gutil) -> gutil.log )
           .pipe gulp.dest paths.webroot + 'js/'
           
gulp.task 'watch', ['sass-css', 'coffee-js'], ->
    gulp.watch paths.scss, ['sass-css']
    gulp.watch paths.coffee, ['coffee-js']
    
 gulp.task 'min:css', ['sass-css'], ->
   return gulp.src [paths.webroot + "css/NavBar_Buttons.css", '!' + paths.webroot + 'css/**/*.min.css']
          .pipe cssmin()
          .pipe rename {suffix: '.min'}
          .pipe gulp.dest paths.webroot + 'css/'
          