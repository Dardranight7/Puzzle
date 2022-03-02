mergeInto(LibraryManager.library, {

  PostResults: function (str,str2) {
    window.parent.SetUser(Pointer_stringify(str),Pointer_stringify(str2));
  },

  GetUser: function () {
    window.parent.GetUser();
  },

});