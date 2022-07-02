using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoreControl.Utilts
{
    internal class Serialize
    {
		public static byte[] ToByteArray(Il2CppSystem.Object obj)
		{
			byte[] result;
			if (obj == null)
			{
				result = null;
			}
			else
			{
				Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				Il2CppSystem.IO.MemoryStream memoryStream = new Il2CppSystem.IO.MemoryStream();
				binaryFormatter.Serialize(memoryStream, obj);
				result = memoryStream.ToArray();
			}
			return result;
		}

		public static byte[] ToByteArray(object obj)
		{
			byte[] result;
			if (obj == null)
			{
				result = null;
			}
			else
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
				binaryFormatter.Serialize(memoryStream, obj);
				result = memoryStream.ToArray();
			}
			return result;
		}
		public static T FromByteArray<T>(byte[] data)
		{
			T result;
			if (data == null)
			{
				result = default(T);
			}
			else
			{
				System.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(data))
				{
					object obj = binaryFormatter.Deserialize(memoryStream);
					result = (T)((object)obj);
				}
			}
			return result;
		}
		public static T IL2CPPFromByteArray<T>(byte[] data)
		{
			T result;
			if (data == null)
			{
				result = default(T);
			}
			else
			{
				Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter binaryFormatter = new Il2CppSystem.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				Il2CppSystem.IO.MemoryStream serializationStream = new Il2CppSystem.IO.MemoryStream(data);
				object obj = binaryFormatter.Deserialize(serializationStream);
				result = (T)((object)obj);
			}
			return result;
		}
		public static T FromIL2CPPToManaged<T>(Il2CppSystem.Object obj)
		{
			return Serialize.FromByteArray<T>(Serialize.ToByteArray(obj));
		}
		public static T FromManagedToIL2CPP<T>(object obj)
		{
			return Serialize.IL2CPPFromByteArray<T>(Serialize.ToByteArray(obj));
		}
		public static byte[] FromIL2CPPArrayToManagedArray(Il2CppSystem.Byte[] obj)
		{
			byte[] array = new byte[obj.Length];
			for (int i = 0; i < obj.Length; i++)
			{
				array[i] = obj[i].m_value;
			}
			return array;
		}
	}
}
