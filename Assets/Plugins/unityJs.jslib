mergeInto(LibraryManager.library, {

  PostResults: function (str) {
    window.PostResults(Pointer_stringify(str));
  },

  GetUser: function () {
    window.parent.GetUser();
  },

});