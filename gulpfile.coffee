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
  
  
gulp.task 'sass-min-css', ->
    return gulp.src paths.scss
           .pipe sass()
           .pipe cssmin()
           .pipe rename {suffix: '.min'}
           .pipe gulp.dest paths.webroot + 'css/'
           
 

gulp.task 'coffee-min-js', ->
    return gulp.src paths.coffee
          .pipe(coffee({bare: true}).on 'error', (gutil) -> gutil.log )
          .pipe uglify()
          .pipe rename {suffix: '.min'}
          .pipe gulp.dest paths.webroot + 'js/'
           
         
 
 gulp.task 'watch', ['sass-min-css', 'coffee-min-js'], ->
    gulp.watch paths.scss, ['sass-min-css']
    gulp.watch paths.coffee, ['coffee-min-js']