// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: cmdid.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
/// <summary>Holder for reflection information generated from cmdid.proto</summary>
public static partial class CmdidReflection {

  #region Descriptor
  /// <summary>File descriptor for cmdid.proto</summary>
  public static pbr::FileDescriptor Descriptor {
    get { return descriptor; }
  }
  private static pbr::FileDescriptor descriptor;

  static CmdidReflection() {
    byte[] descriptorData = global::System.Convert.FromBase64String(
        string.Concat(
          "CgtjbWRpZC5wcm90bypJCgRDU0lEEhAKDEMyU19SZWdpc3RlchAAEg0KCUMy",
          "U19Mb2dpbhABEg0KCUMyU19NYXRjaBACEhEKDUMyU19HYW1lU3RhcnQQAypL",
          "CgRTQ0lEEhAKDFMyQ19SZWdpc3RlchAAEg0KCVMyQ19Mb2dpbhABEg8KC1My",
          "Q19NYXRjaGVkEAISEQoNUzJDX0dhbWVTdGFydBADYgZwcm90bzM="));
    descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
        new pbr::FileDescriptor[] { },
        new pbr::GeneratedClrTypeInfo(new[] {typeof(global::CSID), typeof(global::SCID), }, null, null));
  }
  #endregion

}
#region Enums
public enum CSID {
  /// <summary>
  ///proto3版本中，首成员必须为0，成员不应有相同的值
  /// </summary>
  [pbr::OriginalName("C2S_Register")] C2SRegister = 0,
  [pbr::OriginalName("C2S_Login")] C2SLogin = 1,
  [pbr::OriginalName("C2S_Match")] C2SMatch = 2,
  [pbr::OriginalName("C2S_GameStart")] C2SGameStart = 3,
}

public enum SCID {
  [pbr::OriginalName("S2C_Register")] S2CRegister = 0,
  [pbr::OriginalName("S2C_Login")] S2CLogin = 1,
  [pbr::OriginalName("S2C_Matched")] S2CMatched = 2,
  [pbr::OriginalName("S2C_GameStart")] S2CGameStart = 3,
}

#endregion


#endregion Designer generated code
