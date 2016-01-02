gulp = require 'gulp'
rimraf = require 'rimraf'
concat = require 'gulp-concat'
cssmin = require 'gulp-cssmin'
uglify = require 'gulp-uglify'
project = require './project.json'
sass = require 'gulp-sass'

paths = 
  webroot: "./wwwroot/"
  js: './wwwroot/js/**/*.js'
  minJs: './wwwroot/js/**/*.min.js'
  css: './wwwroot/css/**/*.css'
  minCss: 'css/**/*.min.css'
  scss: './scss/**/*.scss'
  coffee: './coffee/**/*.coffee'
  
  
 gulp.task 'sass-css', ->
    return gulp.src paths.scss
           .pipe sass()
           .pipe gulp.dest paths.webroot + 'css/'
           
 
