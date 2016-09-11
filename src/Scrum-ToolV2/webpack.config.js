/* eslint-disable no-undef */
var path = require('path');
var glob = require("glob");
var webpack = require('webpack');
var autoprefixer = require('autoprefixer');
var postcssSimpleVars = require("postcss-simple-vars")
var postcssNested = require("postcss-nested")
var postcssEasyImport = require("postcss-easy-import")
var postcssMixins = require('postcss-mixins')
var stylelint = require('stylelint')
var CleanWebpackPlugin = require('clean-webpack-plugin');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

var PATHS = {
   app:  path.join(__dirname, 'assets'),
   build: path.join(__dirname, 'wwwroot')
};

var config = {
   entry: {
      app: PATHS.app
   },

   output: {
      path: PATHS.build,
      filename: '[name].bundle.js',
   },

   resolve: {
      modulesDirectories: ['node_modules', 'assets', 'src'],
      extension: [' ', '.js', '.css']
   },

   module: {

      preLoaders: [
         {
            test: /(scss|css)$/,
            loaders: ['postcss'],
				include: PATHS.app
         },
         
         {
             test: /\.jsx?$/,
             loaders: ['eslint'],
             include: PATHS.app
         }
         
      ],

      loaders: [
         {
            test: /\.jsx?$/,
            exclude: /node_modules/,
            loader: "babel-loader"
         },
         {
            test: /(scss|css)$/, 
            loader: ExtractTextPlugin.extract(
               'style',
               'css-loader?sourceMap=true',
               'postcss'
				),
				include:PATHS.app
         },
         {
            test   : /\.(ttf|eot|svg|woff(2)?)(\?[a-z0-9=&.]+)?$/,
            loader : 'file-loader?name=fonts/[name].[hash].[ext]',
         }
      ]

   },

   postcss: function(paths, webpack)  {

      return [
         autoprefixer({
            browsers: ['last 2 versions']
         }),

         postcssEasyImport ({
            path: paths,
            addDependencyTo: webpack,
				prefix: "_"
         }),
			postcssMixins,
         postcssSimpleVars,
         postcssNested
      ]

   },
	
	plugins: [
		new CleanWebpackPlugin(
            [
                './wwwroot/js/',
                './wwwroot/css/'
            ]
      ),
      // Output extracted CSS to a file
      new ExtractTextPlugin('bundle.css', {
         allChunks: true
      })
	]



};

module.exports = config;

/* eslint-enable no-undef */